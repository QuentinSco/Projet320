
namespace FAQU
{
    using System;
    using System.Linq;
    using SkalarkiIO.SDK;
    using System.Timers;
    using FsuipcSdk;

    // To use this class : link OnHardwareEvent(), provide EventClient and Fsuipc objects through Setup();
    class FAQUBrickRefuelling
    {
        private static float FUEL_SLCT_STEP = 0.10f;    // [Kg x 1000]
        private static float FUEL_LOAD_STEP = 0.01f;    // [Kg x 1000]
        private static int TIMER_INTERVAL = 250;        // [ms]

        private enum State { Offline, Off, Selection, Refuelling, Finished, Fault };
        // Offline    : No Skalarki (hardware) connection
        // Off        : Refuelling off
        // Selection  : Choosing fuel quantity
        // Refuelling : well... refuelling
        // Finished   : I've chosen the best words to describe the states
        // Fault      : Important! Skalarki is connected, but no FSUIPC (software) connection

        private State currentState;
        private EventClient hardwareClient;
        private FSUIPCHandler fsuipcHandler;
        private Timer timer;

        private float preselectedFuel;
        private float actualFuel;
        private float maxFuelCapacity;
        //
        private float[] tanksCapacity;
        private static int[] order = {
            FSUIPCHandler.TANK_LEFT_TIP_ID,
            FSUIPCHandler.TANK_RIGHT_TIP_ID,

            FSUIPCHandler.TANK_LEFT_MAIN_ID,
            FSUIPCHandler.TANK_RIGHT_MAIN_ID,

            FSUIPCHandler.TANK_LEFT_AUX_ID,
            FSUIPCHandler.TANK_RIGHT_AUX_ID,

            FSUIPCHandler.TANK_CENTRE_ID
            };

        public FAQUBrickRefuelling()
        {
            SetNextState(State.Offline);
            this.preselectedFuel = 0;
            this.maxFuelCapacity = 0;
            this.actualFuel = 0;
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

            /*try
            {
                hardwareClient.ConnectAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("REFUEL Skalarki.ConnectAsync(): " + e.Message);
                SetNextState(State.Offline);
            }*/
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
            // Filter (Group = Refuel) and (Source = Switch) and (Event = True)
            if ((hardwareEvent.Group == Group.REFUEL) && (hardwareEvent.Source == HardwareSource.Switch) && (hardwareEvent.ValueAsBool() == true))
            {
                switch (currentState)
                {
                    case State.Off:
                        {
                            if (hardwareEvent.Event == Event.POWER)
                            {
                                SetNextState(State.Selection);
                            }
                            break;
                        }
                    case State.Selection:
                        {
                            switch (hardwareEvent.Event)
                            {
                                case Event.POWER:
                                    {
                                        SetNextState(State.Off);
                                        break;
                                    }
                                case Event.INCREASE:
                                    {
                                        IncreasePreselectedFuel();
                                        break;
                                    }
                                case Event.DECREASE:
                                    {
                                        DecreasePreselectedFuel();
                                        break;
                                    }
                                case Event.REFUELING:
                                    {
                                        SetNextState(State.Refuelling);
                                        break;
                                    }
                            }
                            break;
                        }
                    case State.Refuelling:
                        {
                            switch (hardwareEvent.Event)
                            {
                                case Event.POWER:
                                    {
                                        timer.Dispose();
                                        SetNextState(State.Off);
                                        break;
                                    }
                                case Event.REFUELING:
                                    {
                                        timer.Dispose();
                                        SetNextState(State.Selection);
                                        break;
                                    }
                            }
                            break;
                        }
                    case State.Finished:
                        {
                            switch (hardwareEvent.Event)
                            {
                                case Event.POWER:
                                    {
                                        SetNextState(State.Off);
                                        break;
                                    }
                            }
                            break;
                        }
                    case State.Fault:
                        {
                            switch (hardwareEvent.Event)
                            {
                                case Event.POWER:
                                    {
                                        ConnectToFSUIPC();
                                        break;
                                    }
                            }
                            break;
                        }
                }
                UpdateLCD();
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
                case State.Selection:
                case State.Refuelling:
                case State.Finished:
                    {
                        hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, this.actualFuel.ToString());
                        hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, this.preselectedFuel.ToString());
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
                case State.Off:
                    {
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, false);
                        break;
                    }
                case State.Selection:
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
                case State.Refuelling:
                    {
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, true);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, true);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, true);
                        StartRefuel();
                        break;
                    }
                case State.Finished:
                    {
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, true);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGON }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELINGFAULT }, false);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, true);
                        hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, true);
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

        private void IncreasePreselectedFuel()
        {
            this.preselectedFuel += FUEL_SLCT_STEP;
            if (this.preselectedFuel > maxFuelCapacity)
                this.preselectedFuel = maxFuelCapacity;
        }

        private void DecreasePreselectedFuel()
        {
            this.preselectedFuel -= FUEL_SLCT_STEP;
            if (this.preselectedFuel < 0)
                this.preselectedFuel = 0;
        }

        private void StartRefuel()
        {
            this.tanksCapacity = fsuipcHandler.GetTanksCapacity();

            timer = new Timer();
            timer.Interval = TIMER_INTERVAL;

            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += (sender, args) =>
            {
                DoRefuel();
            };

            // Have the timer fire repeated events (true is the default)
            timer.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        private void DoRefuel()
        {
            float[] levels = fsuipcHandler.GetTanksCurrentLevel();
            // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip
            //  0       1           2           3           4           5           6
            float step = (this.preselectedFuel > this.actualFuel) ? FUEL_LOAD_STEP : -FUEL_LOAD_STEP;

            foreach(int idx in order)
            {
                if (IsPossibleToRefuel(step, ref levels[idx], tanksCapacity[idx]))
                {
                    fsuipcHandler.SetNewTankLevel(idx, levels[idx]);
                    break;
                }
            }

            this.actualFuel = fsuipcHandler.GetTotalFuelLevel();
            if ((this.actualFuel >= this.preselectedFuel) && (this.actualFuel <= this.preselectedFuel + FUEL_LOAD_STEP))
            {
                timer.Dispose();
                // FUEL BALANCE
                levels = fsuipcHandler.GetTanksCurrentLevel();
                float aux;
                // MAIN
                aux = levels[FSUIPCHandler.TANK_LEFT_MAIN_ID] + levels[FSUIPCHandler.TANK_RIGHT_MAIN_ID];
                fsuipcHandler.SetNewTankLevel(FSUIPCHandler.TANK_LEFT_MAIN_ID, aux/2);
                fsuipcHandler.SetNewTankLevel(FSUIPCHandler.TANK_RIGHT_MAIN_ID, aux/2);
                // TIP
                aux = levels[FSUIPCHandler.TANK_LEFT_TIP_ID] + levels[FSUIPCHandler.TANK_RIGHT_TIP_ID];
                fsuipcHandler.SetNewTankLevel(FSUIPCHandler.TANK_LEFT_TIP_ID, aux/2);
                fsuipcHandler.SetNewTankLevel(FSUIPCHandler.TANK_RIGHT_TIP_ID, aux/2);
                // AUX
                aux = levels[FSUIPCHandler.TANK_LEFT_AUX_ID] + levels[FSUIPCHandler.TANK_RIGHT_AUX_ID];
                fsuipcHandler.SetNewTankLevel(FSUIPCHandler.TANK_LEFT_AUX_ID, aux/2);
                fsuipcHandler.SetNewTankLevel(FSUIPCHandler.TANK_RIGHT_AUX_ID, aux/2);
                //
                SetNextState(State.Finished);
            }

            UpdateLCD();
        }

        private bool IsPossibleToRefuel(float step, ref float level, float capacity)
        {
            //Console.WriteLine("Check level={0} cap¨={1}", level, capacity);
            float current = capacity * level;
            if (((level != 1 && step>0) || (level !=0 && step<0)) && capacity != 0)
            {
                current = current + step;
                if (current > capacity)
                {
                    current = capacity;
                }
                level = current / capacity;
                //Console.WriteLine("  OUIIIIII¨new level={0}", level);
                return true;
            }
            return false;
        }
    }
}