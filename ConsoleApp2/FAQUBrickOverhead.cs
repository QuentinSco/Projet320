
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
        private bool landing_left_light = false;
        private bool landing_right_light = false;

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
            if (!this.fsuipcHandler.IsConnected)
            {
                result = this.fsuipcHandler.Connect();
                if (result)
                {
                    this.hardwareClient.RegisterEvents(Switches.OVHD.All.Concat(Encoders.OVHD.All));
                    SetNextState(State.Offline);
                }
                else
                    SetNextState(State.Fault);
            }
            else
                SetNextState(State.Offline);
            UpdateLCD();
        }

        public void OnHardwareEvent(IOEvent hardwareEvent, object state)
        {
            bool event_value = hardwareEvent.ValueAsBool();

            switch (currentState)
            {
                case State.Running:
                    {
                        if ((hardwareEvent.Group == Group.LIGHT) && (hardwareEvent.Source == HardwareSource.Switch))
                        {
                            switch (hardwareEvent.Event)
                            {
                                case Event.BEACONLIGHTSOFF:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_BEACON, false);
                                        break;
                                    }
                                case Event.BEACONLIGHTSON:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_BEACON, true);
                                        break;
                                    }
                                case Event.LEFTLANDINGLIGHTON:
                                    {
                                        landing_left_light = event_value;

                                        this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_LANDING, landing_left_light & landing_right_light);
                                        break;
                                    }
                                case Event.RIGHTLANDINGLIGHTON:
                                    {
                                        landing_right_light = event_value;

                                        this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_LANDING, landing_left_light & landing_right_light);
                                        break;
                                    }
                                case Event.NAVLOGOLIGHTSOFF:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_NAVIGATION, false);
                                        break;
                                    }
                                case Event.NAVLOGOLIGHTSON:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_NAVIGATION, true);
                                        break;
                                    }
                                case Event.NOSELIGHTOFF:
                                    {
                                        this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_TAXI, !event_value);
                                        break;
                                    }
                                case Event.RWYLIGHTSOFF:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_RECOGNITION, false);
                                        break;
                                    }
                                case Event.RWYLIGHTSON:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_RECOGNITION, true);
                                        break;
                                    }
                                case Event.STROBESLIGHTSOFF:
                                    {
                                        this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_STROBES, !event_value);
                                        break;
                                    }
                                case Event.WINGLIGHTSOFF:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_WING, false);
                                        break;
                                    }
                                case Event.WINGLIGHTSON:
                                    {
                                        if(event_value)
                                            this.fsuipcHandler.SetNewOvhdLightValue(FSUIPCHandler.LIGHT_INDEX_WING, true);
                                        break;
                                    }
                                // NOT USED IN CURRENT SUPPORTED AIRCRAFT
                                // case Event.LEFTLANDINGLIGHTRETRACTED:
                                // case Event.RIGHTLANDINGLIGHTRETRACTED:
                                // case Event.NOSELIGHTTO:
                                // case Event.STROBESLIGHTSON:
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
        }

        private void UpdateLCD()
        {
            switch (currentState)
            {
                case State.Offline:
                    {
                        // TODO
                        // hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, "   ");
                        // hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "   ");
                        break;
                    }
                case State.Fault:
                    {
                        // TODO
                        // hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, "---");
                        // hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "---");
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
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, true);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, true);
                        break;
                    }
                case State.Fault:
                    {
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, true);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        // hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, false);
                        break;
                    }
            }
        }
    }
}