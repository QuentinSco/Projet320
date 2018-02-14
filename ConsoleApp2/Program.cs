
namespace ConnectToProfilerSDK
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using SkalarkiIO.SDK;
    using System.Timers;
    using FsuipcSdk;

    class Program
    {
        static Fsuipc fsuipc = new Fsuipc();    // Our main fsuipc object!
        static EventClient eventClient;
        static bool result = false;            // Return boolean for FSUIPC method calls
        static int dwResult = -1;              // Variable to hold returned results
        static int dwFSReq = 0;             // Any version of FS is OK
        static int token = -1;
        static float FobT = .0f;


        static void Main(string[] args)
        {

            IPAddress localIP = GetLocalIPAddress();
            using (eventClient = new EventClient(localIP, 53000, (e, s) => { OnHardwareEvent(e, s); }, null))
            {
                eventClient.ConnectionStateChanged += (s, a) =>
                {
                    Console.WriteLine("Client is now {0}", a.Connected ? "connected" : "not connected");
                };

                eventClient.ConnectedDevicesChanged += (s, a) =>
                {
                    Console.WriteLine("Hardware {0} has {1}", a.Device, a.Connected ? "connected" : "disconnected");
                };

                Console.WriteLine("Press <ENTER> to connect to the SkalarkiIO Profiler");
                Console.ReadLine();

                eventClient.ConnectAsync().Wait();

                Console.WriteLine("Press <ENTER> to register an event");
                Console.ReadLine();

                // Register an event
                //var totalEvents = Switches.GLARE.All.Concat(Encoders.GLARE.All);
                var totalEvents = Switches.OVHD.All.Concat(Encoders.OVHD.All);

                eventClient.RegisterEvents(totalEvents);

                //Console.WriteLine("Press <ENTER> to turn on some LEDs");
                //Console.ReadLine();

                /* eventClient.SetOutputs(
                    new[] { Outputs.OVHD.APU.MASTERSW }, true);
                 Console.WriteLine("Press <ENTER> to turn off the LEDs");
                 Console.ReadLine();

                 eventClient.SetOutputs(
                     new[] { Outputs.OVHD.APU.MASTERSW }, false);*/


                eventClient.SetOutputs(new[] { Outputs.OVHD.FIRE.APUFIRE }, true);

                Console.WriteLine("Press <ENTER> to set display text");
                Console.ReadLine();

                Random rnd = new Random();
                //ent = rnd.Next(10, 15);
                //dec = rnd.Next(0, 9);
                fuel = ent.ToString() + "." + dec.ToString();
                eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, false);
                eventClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, "---");
                eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "---");

                Console.WriteLine("Press <ENTER> to connect FSUIPC");
                Console.ReadLine();

                fsuipc.FSUIPC_Initialization();
                result = fsuipc.FSUIPC_Open(dwFSReq, ref dwResult); //Ouverture de la connexion avec FSUIPC
                if (result)
                {
                    Console.WriteLine("FSUIPC initialized");
                }
                else
                    Console.WriteLine("FSUIPC NOT initialized");

                Console.WriteLine("Press <ENTER> to read Fuel");
                Console.ReadLine();

                byte[] buffer = new byte[255];

                result = fsuipc.FSUIPC_Read(0x30C8, 8, ref token, ref dwResult); //Lecture de la masse de l'avion
                result = fsuipc.FSUIPC_Process(ref dwResult);

                result = fsuipc.FSUIPC_Get(ref token, 8, ref buffer);

                double loaded = BitConverter.ToDouble(buffer, 0); //Conversion du tableau de bytes en double
                 
                loaded = (loaded * 32.174049); //Conversion de slug en lbs
                Console.WriteLine("Current mass in lbs: " + loaded);
                 
                result = fsuipc.FSUIPC_Read(0x3BFC, 4, ref token, ref dwResult); //Lecture du Zero Fuel Weight (masse de l'avion + chargement - fuel)
                result = fsuipc.FSUIPC_Process(ref dwResult);
                result = fsuipc.FSUIPC_Get(ref token, ref dwResult);
                float zfw = dwResult / 256; //Conversion demandée par FSUIPC
                Console.WriteLine("ZFW in lbs: " + zfw);

                dwResult = -1;
                token = -1;

                float fob = (float)loaded - zfw; //Calcul du fuel on board (FOB)

                float fobKg = fob * 0.45359237f; //Conversion du FOB en KG

                Console.WriteLine("FOB in lbs: " + fob);
                Console.WriteLine("FOB in kg: " + fobKg);

                FobT = fobKg / 1000;
                actual = FobT.ToString();
                fuel = FobT.ToString();

                //float fob = (float)mass - zfw;

                /*if (result)
                {
                    Console.WriteLine("Fuel on board : " + fob);
                }
                else*/
                if (!result)
                    Console.WriteLine("Read error");

                fsuipc.FSUIPC_Close();

                Console.WriteLine("FSUIPC closed");

                Console.WriteLine("Press <ENTER> to reset display text");
                Console.ReadLine();

                eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "---");
                eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "---");


                Console.WriteLine("Press <ENTER> to disconnect from the SkalarkiIO Profiler");
                Console.ReadLine();

                eventClient.Disconnect();
            }
        }

        private static IPAddress GetLocalIPAddress()
        {
            return Dns.GetHostAddresses(
                Dns.GetHostName())
                .Where((ip) => ip.AddressFamily == AddressFamily.InterNetwork)
                .FirstOrDefault();
        }
    }


// To use this class : link OnHardwareEvent(), provide EventClient and Fsuipc objects through Setup();
    class FAQUBrickRefuelling
    {
        private static final float FUEL_STEP = 0.1;
        private static final int TIMER_INTERVAL = 500;

        private enum State {Offline, Off, Selection, Refuelling, Finished, Fault};
        private State currentState;
        private EventClient hardwareClient;
        private Fsuipc fsuipcClient;
        private Timer timer;

        private float preselectedFuel;
        private float actualFuel;

        FAQUBrickRefuelling()
        {
            SetNextState(State.Offline);
        }

        public bool Setup(EventClient skarlaki, Fsuipc fsuipc)
        {
            this.hardwareClient = skarlaki;
            this.fsuipcClient = fsuipc;

            // TODO if hardwareClient not connected, do something
            // TODO if fsuipc not connected, try to perform connection
            //      if fail --> state = FAULT
            // TODO get aircraft's max fuel capacity

            SetNextState(State.Off);
        }

        public void OnHardwareEvent(IOEvent hardwareEvent, object state)
        {
            // Filter (Group = Refuel) and (Source = Switch) and (Event = True)
            if ((hardwareEvent.Group == Group.REFUEL) && (hardwareEvent.Source == HardwareSource.Switch) && (hardwareEvent.ValueAsBool() == true))
            {
                switch(currentState)
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
                        switch(hardwareEvent.Event)
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
                            }
                            case Event.REFUEL:
                            {
                                SetNextState(State.Refuelling);
                                StartRefuel();
                            }
                        }
                        break;
                    }
                    case State.Refuelling:
                    {
                        switch(hardwareEvent.Event)
                        {
                            case Event.POWER:
                            {
                                SetNextState(State.Off);
                                break;
                            }
                            case Event.REFUEL:
                            {
                                SetNextState(State.Selection);
                                break;
                            }
                        }
                        break;
                    }
                    case State.Finished:
                    {
                        switch(hardwareEvent.Event)
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
                        // TODO
                        break;
                    }
                }
                UpdateLCD();
            }
        }

        private void UpdateLCD()
        {
            switch(currentState)
            {
                case State.Off:
                {
                    hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL,         "   ");
                    hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED,    "   ");
                    break;
                }
                case State.Selection:
                case State.Refuelling:
                case State.Finished:
                {
                    hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL,      this.actualFuel);
                    hardwareClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, this.preselectedFuel);
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
            this.currentState = nextState;

            switch(currentState)
            {
                case State.Off:
                {
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON },     false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT },  false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELON },  false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELFLT }, false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END },       false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT },      false);
                    break;
                }
                case State.Selection:
                {
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON },     true);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT },  false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELON },  false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELFLT }, false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END },       false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT },      true);
                    this.preselectedFuel = ; // TODO get from FSUIPC
                    this.actualFuel = ; // TODO get from FSUIPC
                    break;
                }
                case State.Refuelling:
                {
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON },     true);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT },  false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELON },  true);  // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELFLT }, false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END },       false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT },      true);
                }
                case State.Finished:
                {
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON },     true);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRFAULT },  false);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELON },  false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.REFUELFLT }, false); // todo
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END },       true);
                    hardwareClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT },      true);
                }
            }
        }

        private void StartRefuel()
        {
            timer = new Timer();
            timer.Interval = TIMER_INTERVAL;

            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += (sender, args) =>
            {
                DoRefuel();
            };

            // Have the timer fire repeated events (true is the default)
            tiemr.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        private void IncreasePreselectedFuel()
        {
            this.preselectedFuel += FUEL_STEP;
        }

        private void DecreasePreselectedFuel()
        {
            this.preselectedFuel -= FUEL_STEP;
        }

        private void DoRefuel()
        {
            if(this.preselectedFuel > this.actualFuel)
            {
                this.actualFuel += FUEL_STEP;
            }
            else if (this.preselectedFuel < this.actualFuel)
            {
                this.actualFuel -= FUEL_STEP;
            }
            else
            {
                timer.Dispose();
                SetNextState(State.Finished);
            }
            UpdateFSUIPC();
            UpdateLCD();
        }

        private void UpdateFSUIPC()
        {
            // TODO send to FSUIPC the actual value
            // TODO if no connection --> stete = FAULT and (timer.dispose()!)
        }
    }

}

        /*private static void OnHardwareEvent(IOEvent hardwareEvent, object state)
        {

            switch (hardwareEvent.Source)
            {
                case HardwareSource.Switch:
                    Console.WriteLine("Received button {0} {1} state {2}",
                        hardwareEvent.Event,
                        hardwareEvent.Group,
                        hardwareEvent.ValueAsBool() ? "pressed" : "released");
                    break;
                case HardwareSource.Encoder:
                    Console.WriteLine("Received encoder {0} {1} state {2}",
                        hardwareEvent.Event,
                        hardwareEvent.Group,
                        hardwareEvent.ValueAsBool() ? "turned clockwise" : "turned counter clockwise");
                    break;
                case HardwareSource.Axis:
                    Console.WriteLine("Received an axis ({0}) event with value {1}",
                        hardwareEvent.Event,
                        hardwareEvent.ValueAsInt());
                    break;
                default:
                    Console.WriteLine("Received an event {0} state {1}", hardwareEvent.Event, hardwareEvent.ValueAsBool());
                    break;
            }
        }*/