
namespace FAQU
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
        static EventClient eventClient;         // Skalarki HW object

        private static IPAddress GetLocalIPAddress()
        {
            return Dns.GetHostAddresses(
                Dns.GetHostName())
                .Where((ip) => ip.AddressFamily == AddressFamily.InterNetwork)
                .FirstOrDefault();
        }

        static void Main(string[] args)
        {
            FAQUBrickRefuelling fAQU = new FAQUBrickRefuelling();
            IPAddress localIP = GetLocalIPAddress();

            using (eventClient = new EventClient(localIP, 53000, (e, s) => { fAQU.OnHardwareEvent(e, s); }, null))
            {
                eventClient.ConnectedDevicesChanged += (s, a) =>
                {
                    Console.WriteLine("Hardware {0} has {1}", a.Device, a.Connected ? "connected" : "disconnected");
                };

                fAQU.Setup(eventClient, fsuipc);

                Console.ReadLine();
                eventClient.Disconnect();
            }
        }
    }

    // To use this class : link OnHardwareEvent(), provide EventClient and Fsuipc objects through Setup();
    class FAQUBrickRefuelling
    {
        private static float FUEL_STEP = 0.1f;
        private static int TIMER_INTERVAL = 500;

        private enum State { Offline, Off, Selection, Refuelling, Finished, Fault };
        private State currentState;
        private EventClient hardwareClient;
        private Fsuipc fsuipcClient;
        private FSUIPCHandler fsuipcHandler;
        private Timer timer;

        private float preselectedFuel;
        private float actualFuel;
        private float maxFuelCapacity;

        public FAQUBrickRefuelling()
        {
            SetNextState(State.Offline);
            this.preselectedFuel = 0;
            this.maxFuelCapacity = 0;
            this.actualFuel = 0;
        }


        public void Setup(EventClient skarlaki, Fsuipc fsuipc)
        {
            this.hardwareClient = skarlaki;
            this.fsuipcClient = fsuipc;
            this.fsuipcHandler = new FSUIPCHandler(this.fsuipcClient);

            this.hardwareClient.ConnectionStateChanged += (s, a) =>
            {
                bool result;
                int dwFSReq = 0;            // Any version of FS is OK
                int dwResult = -1;          // Variable to hold returned results
                if (a.Connected)
                {
                    if(this.currentState == State.Offline)
                    {
                        this.fsuipcClient.FSUIPC_Initialization();

                        result = this.fsuipcClient.FSUIPC_Open(dwFSReq, ref dwResult);
                        if(result)
                        {
                            this.maxFuelCapacity = GetMaxFuelCapacity();
                            this.hardwareClient.RegisterEvents(Switches.OVHD.All.Concat(Encoders.OVHD.All));
                            SetNextState(State.Off);
                        }
                        else
                        {
                            FSUIPCHandler.PrintErrorCode(dwResult);
                            SetNextState(State.Fault);
                        }
                    }
                }
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
                                        StartRefuel();
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
                                        SetNextState(State.Off);
                                        break;
                                    }
                                case Event.REFUELING:
                                    {
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
                            // TODO
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
                        this.preselectedFuel = GetActualFuel();
                        this.actualFuel = GetActualFuel();
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
            timer.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        private void IncreasePreselectedFuel()
        {
            this.preselectedFuel += FUEL_STEP;
            if (this.preselectedFuel > maxFuelCapacity)
                this.preselectedFuel = maxFuelCapacity;
        }

        private void DecreasePreselectedFuel()
        {
            this.preselectedFuel -= FUEL_STEP;
            if (this.preselectedFuel < 0)
                this.preselectedFuel = 0;
        }

        private void DoRefuel()
        {
            UpdateFSUIPC();
            // TODO: update this.actualFuel by calling GetTotalFuelLevel();

            if (this.preselectedFuel > this.actualFuel)
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
            UpdateLCD();
        }

        private float GetActualFuel()
        {
            bool result = false;            // Return boolean for FSUIPC method calls
            int dwResult = -1;              // Variable to hold returned results
            int token = -1;

            byte[] buffer = new byte[255];

            result = this.fsuipcClient.FSUIPC_Read(0x30C8, 8, ref token, ref dwResult); //Lecture de la masse de l'avion
            result = this.fsuipcClient.FSUIPC_Process(ref dwResult);

            result = this.fsuipcClient.FSUIPC_Get(ref token, 8, ref buffer);

            double loaded = BitConverter.ToDouble(buffer, 0); //Conversion du tableau de bytes en double

            loaded = (loaded * 32.174049); //Conversion de slug en lbs
            Console.WriteLine("Current mass in lbs: " + loaded);

            result = this.fsuipcClient.FSUIPC_Read(0x3BFC, 4, ref token, ref dwResult); //Lecture du Zero Fuel Weight (masse de l'avion + chargement - fuel)
            result = this.fsuipcClient.FSUIPC_Process(ref dwResult);
            result = this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);
            float zfw = dwResult / 256; //Conversion demandée par FSUIPC
            Console.WriteLine("ZFW in lbs: " + zfw);

            float fob = (float)loaded - zfw; //Calcul du fuel on board (FOB)

            float fobKg = fob * 0.45359237f; //Conversion du FOB en KG

            return fobKg / 1000;
        }

        private float GetMaxFuelCapacity()
        {
            //return FSUIPCHandler.GetTotalFuelCapacity(this.fsuipcClient);


            bool result = false;            // Return boolean for FSUIPC method calls
            int dwResult = -1;              // Variable to hold returned results
            int token = -1;
            float capacityUSGal = 0;
            float capacityL = 0;
            float fuelweightLbsGal = 0;
            float fuelweightKgL = 0;
            Int32[] AddrTab = new Int32[7] { 0x0B78, 0x0B80, 0x0B88, 0x0B90, 0x0B98, 0x0BA0, 0x0BA8 }; //0 : centre, 1 : aile gauche, 4 : aile droite
            foreach (Int32 adr in AddrTab)
            {
                result = this.fsuipcClient.FSUIPC_Read(adr, 4, ref token, ref dwResult); //Lecture du Zero Fuel Weight (masse de l'avion + chargement - fuel)
                result = this.fsuipcClient.FSUIPC_Process(ref dwResult);
                result = this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);
                capacityUSGal += dwResult;
            }

            result = this.fsuipcClient.FSUIPC_Read(0x0AF4, 4, ref token, ref dwResult); //Lecture de la masse volumique du carburant
            result = this.fsuipcClient.FSUIPC_Process(ref dwResult);
            result = this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);
            dwResult /= 256; //opération requise par FSUIPC

            fuelweightLbsGal = dwResult;

            fuelweightKgL = 0.1198264f * fuelweightLbsGal;

            capacityL = 3.78541f * capacityUSGal;

            Console.WriteLine("max capacity en UsGal : " + capacityUSGal);
            Console.WriteLine("max capacity en Kg : " + (fuelweightKgL * capacityL));

            return capacityL;
        }

        private void UpdateFSUIPC()
        {
            // TODO send to FSUIPC the actual value
            // TODO if no connection --> stete = FAULT and (timer.dispose()!)

            float[] levels = FSUIPCHandler.GetTanksCurrentLevel(fsuipcClient);
            float[] capacities = FSUIPCHandler.GetTanksCapacity(fsuipcClient);

            // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip
            //  0       1           2           3           4           5           6
            int[] order = {0, 1, 4, 2, 5, 3, 6};
            foreach(int idx in order)
            {
                if(IsPossibleToRefuel(FUEL_STEP, ref levels[i], capacities[i]))
                {
                    SetNewTankLevel(fsuipcClient, i, levels[i]);
                    break;
                }
            }
        }

        private bool IsPossibleToRefuel(float step, float level, float capacity)
        {
            float current = capacity * levels; // TODO maybe it's levels/100
            if (levels != 1)                   // TODO either 1 or 100 ?
            {
                current = current + step;
                if (current > capacity)
                {
                    current = capacity;
                }
                level = current / capacity;    // TODO * 100 ?
                return true;
            }
            return false;
        }

        public void TestToCarryOut()
        {
            // Comparing the two methods

            float[] capacities = fsuipcHandler.GetTanksCapacity(this.fsuipcClient);
            float[] levels = fsuipcHandler.GetTanksCurrentLevel(this.fsuipcClient);

            float current = 0;
            float capacity = 0;
            for(int i=0; i<7; i++)
            {
                current = current + (capacities[i] * levels[i]); // TODO: maybe it's level/100
                capacity = capacity + capacities[i];
            }

            float quentinActual = GetActualFuel();
            float quentinMax    = GetMaxFuelCapacity();

            Console.WriteLine(">> Comparing the methods")
            Console.WriteLine(" float GetActualFuel() : {0} in Kg x 1000", quentinActual);
            Console.WriteLine(" float[] Individually  : {0} in Kg x 1000", total;
            Console.WriteLine(".");
            Console.WriteLine(" float GetMaxFuelCapacity() : {0} in liters?", quentinMax);
            Console.WriteLine(" float GetTanksCapacity()   : {0} in Kg x 1000", capacity);
        }
    }

    class FSUIPCHandler
    {
        public fsuipc fsuipcClient;
        private bool isConnected;

        //
        private bool result;         // Return boolean for FSUIPC method calls
        private int dwResult = -1;   // Variable to hold returned results
        private int token = -1;
        private byte[] buffer = new byte[255];
        //
        private static readonly Int32[] AddrTabTotalFuelCapacity = new Int32[7] { 0x0B78, 0x0B80, 0x0B88, 0x0B90, 0x0B98, 0x0BA0, 0x0BA8 }; // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip

        private static readonly Int32[] AddrTabCurrentFuelLevels = new Int32[7] { 0x0B74, 0x0B7C, 0x0B84, 0x0B8C, 0x0B94, 0x0B9C, 0x0BA4 }; // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip

        // TODO: add exceptions for results = false
        // TODO: encapsulate fsuipcClient here

        public FSUIPCHandler(fsuipc fsuipcClient)
        {
            this.fsuipcClient = fsuipcClient;
            this.isConnected = false;
        }

        public bool IsConnected
        {
            public get { return this.isConnected; }
            private set { }
        }

        public bool Connect()
        {
            // TODO
        }

        public float[] GetTanksCapacity()
        {
            // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip
            // Unit: Kg x 1000

            float[] tanks = new float[7];
            int i = 0;
            float fuelDensity; // pounds per US gallon

            // Fuel weight as pounds per gallon
            result = this.fsuipcClient.FSUIPC_Read(0x0AF4, 4, ref token, ref dwResult);
            result &= this.fsuipcClient.FSUIPC_Process(ref dwResult);
            result &= this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);

            if(result)
                fuelDensity = dwResult / 256.0f;
            else
            {
                fuelDensity = 0;
                PrintErrorCode(dwResult);
            }

            // Tanks capacity in US Gallons
            foreach (Int32 adr in AddrTabTotalFuelCapacity)
            {
                result = this.fsuipcClient.FSUIPC_Read(adr, 4, ref token, ref dwResult);
                result &= this.fsuipcClient.FSUIPC_Process(ref dwResult);
                result &= this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);

                if(result)
                {
                    // Convert from US Gallons to Pounds then to Kg then to 1000xKg
                    // 1000 x Kg = [gal] * [lbs/gal] * [kg/lbs] / []
                    tanks[i] = dwResult * fuelDensity * 0.453592f / 1000.0f;
                }
                else
                {
                    tanks[i] = 0;
                    PrintErrorCode(dwResult);
                }
            }

            return tanks;
        }

        public float[] GetTanksCurrentLevel()
        {
            // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip
            // Unit: percentage

            float[] tanks = new float[7];
            int i = 0;

            foreach (Int32 adr in AddrTabCurrentFuelLevels)
            {
                result = this.fsuipcClient.FSUIPC_Read(adr, 4, ref token, ref dwResult);
                result &= this.fsuipcClient.FSUIPC_Process(ref dwResult);
                result &= this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);

                if(result)
                {
                    tanks[i] = dwResult / 128.0f / 65536.0f;
                }
                else
                {
                    tanks[i] = 0;
                    PrintErrorCode(dwResult);
                }
            }

            return tanks;
        }


        public float GetTotalFuelCapacity()
        {
            // Unit: Kg x 1000

            float[] capacities = GetTanksCapacity();
            float total = 0;

            foreach (float capacity in capacities)
                total = total + capacity;

            return total;
        }

        public float GetTotalFuelLevel()
        {
            float[] capacities = FSUIPCHandler.GetTanksCapacity();
            float[] levels = FSUIPCHandler.GetTanksCurrentLevel();

            float current = 0;
            float capacity = 0;
            for(int i=0; i<7; i++)
                current = current + (capacities[i] * levels[i]);

            return current;
        }

        public double GetCurrentLoadedWeight()
        {
            // Unit: lbs
            double loaded;

            // Current loaded weight in lbs.
            result = this.fsuipcClient.FSUIPC_Read(0x30C0, 8, ref token, ref dwResult);
            result &= this.fsuipcClient.FSUIPC_Process(ref dwResult);
            result &= this.fsuipcClient.FSUIPC_Get(ref token, 8, ref buffer);

            if (result)
            {
                // Conversion du tableau de bytes en double (Float64)
                loaded = BitConverter.ToDouble(buffer, 0);
            }
            else
            {
                loaded = 0;
                PrintErrorCode(dwResult);
            }

            return loaded;
        }

        public void SetNewTankLevel(int tank_id, float level)
        {
            level = level * 128 * 65536;
            result = this.fsuipcClient.FSUIPC_Write(AddrTabCurrentFuelLevels[tank_id], level, token, dwResult);
            result &= this.fsuipcClient.FSUIPC_Process(ref dwResult);

            if(!result)
                PrintErrorCode(dwResult);
        }


        public static void PrintErrorCode(int code)
        {
            Console.WriteLine(translateResultCode(code));
        }

        public static string translateResultCode(int code) 
        {
            switch (code) 
            {

                case Fsuipc.FSUIPC_ERR_OK:
                    return code + " = FSUIPC_ERR_OK";
                    break;

                case Fsuipc.FSUIPC_ERR_OPEN:
                    return code + " = FSUIPC_ERR_OPEN";
                    break;

                case Fsuipc.FSUIPC_ERR_NOFS:
                    return code + " = FSUIPC_ERR_NOFS";
                    break;

                case Fsuipc.FSUIPC_ERR_REGMSG:
                    return code + " = FSUIPC_ERR_REGMSG";
                    break;

                case Fsuipc.FSUIPC_ERR_ATOM:
                    return code + " = FSUIPC_ERR_ATOM";
                    break;

                case Fsuipc.FSUIPC_ERR_MAP:
                    return code + " = FSUIPC_ERR_MAP";
                    break;

                case Fsuipc.FSUIPC_ERR_VIEW:
                    return code + " = FSUIPC_ERR_VIEW";
                    break;

                case Fsuipc.FSUIPC_ERR_VERSION:
                    return code + " = FSUIPC_ERR_VERSION";
                    break;

                case Fsuipc.FSUIPC_ERR_WRONGFS:
                    return code + " = FSUIPC_ERR_WRONGFS";
                    break;

                case Fsuipc.FSUIPC_ERR_NOTOPEN:
                    return code + " = FSUIPC_ERR_NOTOPEN";
                    break;

                case Fsuipc.FSUIPC_ERR_NODATA:
                    return code + " = FSUIPC_ERR_NODATA";
                    break;

                case Fsuipc.FSUIPC_ERR_TIMEOUT:
                    return code + " = FSUIPC_ERR_TIMEOUT";
                    break;

                case Fsuipc.FSUIPC_ERR_SENDMSG:
                    return code + " = FSUIPC_ERR_SENDMSG";
                    break;

                case Fsuipc.FSUIPC_ERR_DATA:
                    return code + " = FSUIPC_ERR_DATA";
                    break;

                case Fsuipc.FSUIPC_ERR_RUNNING:
                    return code + " = FSUIPC_ERR_RUNNING";
                    break;

                case Fsuipc.FSUIPC_ERR_SIZE:
                    return code + " = FSUIPC_ERR_SIZE";
                    break;

                case Fsuipc.FSUIPC_ERR_BUFOVERFLOW:
                    return code + " = FSUIPC_ERR_BUFOVERFLOW";
                    break;

                default:
                    return code.ToString();
                    break;

            }
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

// TODO play a song during refuelling