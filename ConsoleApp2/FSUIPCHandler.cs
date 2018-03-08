
namespace FAQU
{
    using System;
    using FsuipcSdk;

    class FSUIPCHandler
    {
        public Fsuipc fsuipcClient;
        private bool isConnected;
        //
        private bool result;         // Return boolean for FSUIPC method calls
        private int dwResult = -1;   // Variable to hold returned results
        private int token = -1;
        private byte[] buffer = new byte[255];
        //
        // centre ; left main ; left aux ; left tip ; right main ; right aux ; right tip
        private static readonly Int32[] AddrTabTotalFuelCapacity = new Int32[7] { 0x0B78, 0x0B80, 0x0B88, 0x0B90, 0x0B98, 0x0BA0, 0x0BA8 };
        public static readonly Int32[] AddrTabCurrentFuelLevels = new Int32[7] { 0x0B74, 0x0B7C, 0x0B84, 0x0B8C, 0x0B94, 0x0B9C, 0x0BA4 };
        public static readonly int TANK_CENTRE_ID = 0;
        public static readonly int TANK_LEFT_MAIN_ID = 1;
        public static readonly int TANK_LEFT_AUX_ID = 2;
        public static readonly int TANK_LEFT_TIP_ID = 3;
        public static readonly int TANK_RIGHT_MAIN_ID = 4;
        public static readonly int TANK_RIGHT_AUX_ID = 5;
        public static readonly int TANK_RIGHT_TIP_ID = 6;
        //
        private static readonly Int32 LIGHT_ADDRESS = 0x0D0C;
        public static readonly int LIGHT_INDEX_NAVIGATION = 0;
        public static readonly int LIGHT_INDEX_BEACON = 1;
        public static readonly int LIGHT_INDEX_LANDING = 2;
        public static readonly int LIGHT_INDEX_TAXI = 3;
        public static readonly int LIGHT_INDEX_STROBES = 4;
        public static readonly int LIGHT_INDEX_INSTRUMENTS = 5;
        public static readonly int LIGHT_INDEX_RECOGNITION = 6;
        public static readonly int LIGHT_INDEX_WING = 7;
        public static readonly int LIGHT_INDEX_LOGO = 8;
        public static readonly int LIGHT_INDEX_CABIN = 9;
        public struct StructLightsFSUIPC
        {
            public char bits;
            public StructLightsFSUIPC(char initVal)
            {
                this.bits = initVal;
            }
            public bool this[int index]
            {
                get { return ((bits & (1 << index)) != 0); }
                set { bits = (value) ? (bits |= (char)(1 << index)) : (bits &= (char)~(1 << index)); }
            }
        };
        private StructLightsFSUIPC ovhd_lights;
        //
        // TODO: add exceptions for results = false

        public FSUIPCHandler()
        {
            this.fsuipcClient = new Fsuipc();
            this.isConnected = false;
            this.ovhd_lights = new StructLightsFSUIPC((char)0);
        }

        public bool IsConnected
        {
            get { return this.isConnected; }
            private set { }
        }

        public bool Connect()
        {
            int dwFSReq=0;
            bool result;
            fsuipcClient.FSUIPC_Initialization();
            result = fsuipcClient.FSUIPC_Open(dwFSReq, ref dwResult);

            if(!result)
                PrintErrorCode(dwResult);

            return result;
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

                if (result)
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
                
                i++;
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
                    //tanks.Add(dwResult / 128.0f / 65536.0f);
                }
                else
                {
                    tanks[i] = 0;
                    //tanks.Add(0);
                    PrintErrorCode(dwResult);
                }
                i++;
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
            float[] capacities = GetTanksCapacity();
            float[] levels = GetTanksCurrentLevel();

            float current = 0;
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
                
            result = fsuipcClient.FSUIPC_Write(AddrTabCurrentFuelLevels[tank_id], (int)level, ref token, ref dwResult);
            result &= fsuipcClient.FSUIPC_Process(ref dwResult);

            if(!result)
                PrintErrorCode(dwResult);
        }

        // LIGHTS
        public void SetNewOvhdLightValue(int light_id, bool value)
        {
            ovhd_lights[light_id] = value;

            result = fsuipcClient.FSUIPC_Write(LIGHT_ADDRESS, ovhd_lights.bits, ref token, ref dwResult);
            result &= fsuipcClient.FSUIPC_Process(ref dwResult);

            if(!result)
                PrintErrorCode(dwResult);
        }

        public bool GetOvhdLightValue(int light_id)
        {
            result = this.fsuipcClient.FSUIPC_Read(LIGHT_ADDRESS, 1, ref token, ref dwResult);
            result &= this.fsuipcClient.FSUIPC_Process(ref dwResult);
            result &= this.fsuipcClient.FSUIPC_Get(ref token, ref dwResult);

            if(result)
            {
                ovhd_lights.bits = (char)dwResult;
            }
            else
            {
                ovhd_lights.bits = (char)0;
                PrintErrorCode(dwResult);
            }

            return ovhd_lights[light_id];
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

                case Fsuipc.FSUIPC_ERR_OPEN:
                    return code + " = FSUIPC_ERR_OPEN";

                case Fsuipc.FSUIPC_ERR_NOFS:
                    return code + " = FSUIPC_ERR_NOFS";

                case Fsuipc.FSUIPC_ERR_REGMSG:
                    return code + " = FSUIPC_ERR_REGMSG";

                case Fsuipc.FSUIPC_ERR_ATOM:
                    return code + " = FSUIPC_ERR_ATOM";

                case Fsuipc.FSUIPC_ERR_MAP:
                    return code + " = FSUIPC_ERR_MAP";

                case Fsuipc.FSUIPC_ERR_VIEW:
                    return code + " = FSUIPC_ERR_VIEW";

                case Fsuipc.FSUIPC_ERR_VERSION:
                    return code + " = FSUIPC_ERR_VERSION";

                case Fsuipc.FSUIPC_ERR_WRONGFS:
                    return code + " = FSUIPC_ERR_WRONGFS";

                case Fsuipc.FSUIPC_ERR_NOTOPEN:
                    return code + " = FSUIPC_ERR_NOTOPEN";

                case Fsuipc.FSUIPC_ERR_NODATA:
                    return code + " = FSUIPC_ERR_NODATA";

                case Fsuipc.FSUIPC_ERR_TIMEOUT:
                    return code + " = FSUIPC_ERR_TIMEOUT";

                case Fsuipc.FSUIPC_ERR_SENDMSG:
                    return code + " = FSUIPC_ERR_SENDMSG";

                case Fsuipc.FSUIPC_ERR_DATA:
                    return code + " = FSUIPC_ERR_DATA";

                case Fsuipc.FSUIPC_ERR_RUNNING:
                    return code + " = FSUIPC_ERR_RUNNING";

                case Fsuipc.FSUIPC_ERR_SIZE:
                    return code + " = FSUIPC_ERR_SIZE";

                case Fsuipc.FSUIPC_ERR_BUFOVERFLOW:
                    return code + " = FSUIPC_ERR_BUFOVERFLOW";

                default:
                    return code.ToString();
            }
        }
    }
}