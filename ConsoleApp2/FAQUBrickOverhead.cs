
namespace FAQU
{
    using System;
    using System.Linq;
    using SkalarkiIO.SDK;
    using System.Timers;
    using FsuipcSdk;

    // To use this class : link OnHardwareEvent(), provide EventClient and Fsuipc objects through Setup();
    class FAQUBrickOverhead
    {
        private enum State { Offline, Running, Fault };
        // Offline    : No Skalarki (hardware) connection
        // Running    : I've chosen the best words to describe the states
        // Fault      : Important! Skalarki is connected, but no FSUIPC (software) connection
        //
        private State currentState;
        private EventClient hardwareClient;
        private FSUIPCHandler fsuipcHandler;
        private Fsuipc fsuipcClient = new Fsuipc();
        //

        public FAQUBrickOverhead()
        {
            SetNextState(State.Offline);
        }

        public void Setup(EventClient skarlaki, FSUIPCHandler fsuipc)
        {
            this.hardwareClient = skarlaki;
            this.fsuipcHandler = fsuipc;

            this.hardwareClient.ConnectionStateChanged += (s, skalarki) =>
            {
                if (skalarki.Connected)
                    ConnectToFSUIPC();
                else
                    SetNextState(State.Offline);
            };

            try
            {
                hardwareClient.ConnectAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Skalarki.ConnectAsync(): " + e.Message);
                SetNextState(State.Offline);
            }
        }

        private void ConnectToFSUIPC()
        {
            bool result = false;
            if(!this.fsuipcHandler.IsConnected)
            {
                result = this.fsuipcHandler.Connect();
                if(result)
                {
                    // TestToCarryOut();
                    this.maxFuelCapacity = fsuipcHandler.GetTotalFuelCapacity();
                    this.hardwareClient.RegisterEvents(Switches.OVHD.All.Concat(Encoders.OVHD.All));
                    SetNextState(State.Off);
                }
                else
                    SetNextState(State.Fault);
            }
            else
                SetNextState(State.Off);
            UpdateLCD();
        }

        public void OnHardwareEvent(IOEvent hardwareEvent, object state)
        {
            bool event_value = hardwareEvent.ValueAsBool();

            switch (currentState)
            {
                case State.Running:
                    {
                        // TODO: all the work
                        if((hardwareEvent.Group == Group.LIGHT) && (HardwareSource.Source == HardwareSource.Switch))
                        {
                            switch(hardwareEvent.Event)
                            {
                                // Skalarki    FSUIPC
                                // NAVLOGO      NAVLOGO
                                // Beacon       BEACON
                                // LAND LEFT    LANDING
                                // LAND RIGHT   LANDING
                                // Taxi         NOSE
                                // Strobes      Strobe
                                // --           Instruments
                                // Recognition  RunwayTurnOff
                                // Wing         WING
                                // --           Logo
                                // --           Cabin

                                case Event.BEACONLIGHTSOFF:
                                {
                                }
                                case Event.BEACONLIGHTSON:
                                {
                                }
                                case Event.LEFTLANDINGLIGHTON:
                                {
                                }
                                case Event.LEFTLANDINGLIGHTRETRACTED:
                                {
                                }
                                case Event.RIGHTLANDINGLIGHTON:
                                {
                                }
                                case Event.RIGHTLANDINGLIGHTRETRACTED:
                                {
                                }
                                case Event.NAVLOGOLIGHTSOFF:
                                {
                                }
                                case Event.NAVLOGOLIGHTSON:
                                {
                                }
                                case Event.NOSELIGHTOFF:
                                {
                                }
                                case Event.NOSELIGHTTO:
                                {
                                }
                                case Event.RWYLIGHTSOFF:
                                {
                                }
                                case Event.RWYLIGHTSON:
                                {
                                }
                                case Event.STROBESLIGHTSOFF:
                                {
                                }
                                case Event.STROBESLIGHTSON:
                                {
                                }
                                case Event.WINGLIGHTSOFF:
                                {
                                }
                                case Event.WINGLIGHTSON:
                                {
                                }
                            }
                        }
                        UpdateLCD();
                        break;
                    }
                case State.Fault:
                    {
                        ConnectToFSUIPC();
                        break;
                    }
            }


            // Filter (Group = Refuel) and (Source = Switch) and (Event = True)
            if ((hardwareEvent.Group == Group.REFUEL) && (hardwareEvent.Source == HardwareSource.Switch) && (hardwareEvent.ValueAsBool() == true))
            {
                
            }
        }

        private void UpdateLCD()
        {
            switch (currentState)
            {
                case State.Off:
                    {
                        hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, "   ");
                        hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "   ");
                        break;
                    }
                case State.Fault:
                    {
                        hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, "---");
                        hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "---");
                        break;
                    }
            }
        }

        private void SetNextState(State nextState)
        {
            Console.WriteLine("Next state : " + nextState);
            this.currentState = nextState;

            switch (currentState)
            {
                case State.Running:
                    {
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, true);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, true);
                        this.preselectedFuel = fsuipcHandler.GetTotalFuelLevel();
                        this.actualFuel = fsuipcHandler.GetTotalFuelLevel();
                        break;
                    }
                case State.Fault:
                    {
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, true);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, false);
                        break;
                    }
            }
        }
    }
}