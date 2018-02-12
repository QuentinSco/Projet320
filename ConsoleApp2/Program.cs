
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
        static Fsuipc fsuipc = new Fsuipc();	// Our main fsuipc object!
        static string fuel = "10.0";
        static string actual = "10.0";
        static int entactu = 10;
        static int decactu = 0;
        static int ent = 10;
        static int dec = 0;
        static bool IsOn = false;
        static EventClient eventClient;
        static bool result = false;            // Return boolean for FSUIPC method calls
        static int dwResult = -1;              // Variable to hold returned results
        static int dwFSReq = 0;				// Any version of FS is OK
        static int token = -1;
        static float FobT = .0f;
        static void Main(string[] args)
        {

            var localIP = GetLocalIPAddress();
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
        private static void OnHardwareEvent(IOEvent hardwareEvent, object state)
        {

            /*switch (hardwareEvent.Source)
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
            }*/
            if (hardwareEvent.Source == HardwareSource.Switch && hardwareEvent.ValueAsBool() == true)
            {
                if(hardwareEvent.Event == Event.INCREASE && hardwareEvent.Group == Group.REFUEL)
                {
                    IncreasePreselectedFuel();
                    fuel = ent.ToString() + "." + dec.ToString();
                    eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, fuel);
                }
                if (hardwareEvent.Event == Event.DECREASE && hardwareEvent.Group == Group.REFUEL)
                {
                    DecreasePreselectedFuel();
                    fuel = ent.ToString() + "." + dec.ToString();
                    eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, fuel);
                }
                if(hardwareEvent.Event == Event.REFUELING && hardwareEvent.ValueAsBool() == true)
                {
                    StartRefuel(fuel);
                }
                if(hardwareEvent.Event == Event.POWER && hardwareEvent.Group == Group.REFUEL/* && hardwareEvent.ValueAsBool() == true*/)
                {
                    IsOn = !IsOn;
                    if(IsOn)
                    {
                        eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, true);
                        eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, true);
                        eventClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, actual);
                        eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, fuel);
                    }
                    if(!IsOn)
                    {
                        eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.PWRON }, false);
                        eventClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, "   ");
                        eventClient.SetDisplayText(Displays.OVHD.REFUEL.PRESELECTED, "   ");
                        eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, false);
                        eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.CKPT }, false);
                    }
                }
            }
        }

        protected static void IncreasePreselectedFuel()
        {
            if (dec == 9)
            {
                ent++;
                dec = 0;
            }
            else
                dec++;
        }

        protected static void IncreaseFuel()
        {
            if (decactu == 9)
            {
                entactu++;
                decactu = 0;
            }
            else
                decactu++;
        }

        protected static void DecreasePreselectedFuel()
        {
            if (dec == 0)
            {
                ent--;
                dec = 9;
            }
            else
                dec--;
        }

        protected static void RefreshActual()
        {
            actual = entactu.ToString() + "." + decactu.ToString();
            eventClient.SetDisplayText(Displays.OVHD.REFUEL.ACTUAL, actual);
        }

        private static Timer aTimer;

        protected static void StartRefuel(string obj)
        {
            aTimer = new Timer();
            aTimer.Interval = 500;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, args) =>
            {
                if(actual!=fuel)
                {
                    IncreaseFuel();
                    RefreshActual();
                }
                else
                {
                    eventClient.SetOutputs(new[] { Outputs.OVHD.REFUEL.END }, true);
                    aTimer.Dispose();
                }
                    
            };

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
        }


        private static IPAddress GetLocalIPAddress()
        {
            return Dns.GetHostAddresses(
                Dns.GetHostName())
                .Where((ip) => ip.AddressFamily == AddressFamily.InterNetwork)
                .FirstOrDefault();
        }
    }
}
