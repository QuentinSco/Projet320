
namespace ConnectToProfilerSDK
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using SkalarkiIO.SDK;

    class Program
    {
        static void Main(string[] args)
        {
            var localIP = GetLocalIPAddress();
            using (var eventClient = new EventClient(localIP, 53000, (e, s) => { OnHardwareEvent(e, s); }, null))
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
                var totalEvents = Switches.GLARE.All.Concat(Encoders.GLARE.All);
                eventClient.RegisterEvents(totalEvents);

                Console.WriteLine("Press <ENTER> to turn on some LEDs");
                Console.ReadLine();

                // Turn on some leds
                eventClient.SetOutputs(
                    new[] { Outputs.GLARE.EFIS.CS.ILS, Outputs.GLARE.EFIS.CS.NDB, Outputs.GLARE.EFIS.CS.QFE }, true);

                Console.WriteLine("Press <ENTER> to turn off the LEDs");
                Console.ReadLine();

                eventClient.SetOutputs(
                    new[] { Outputs.GLARE.EFIS.CS.ILS, Outputs.GLARE.EFIS.CS.NDB, Outputs.GLARE.EFIS.CS.QFE }, false);

                Console.WriteLine("Press <ENTER> to set display text");
                Console.ReadLine();

                eventClient.SetDisplayText(Displays.GLARE.FCU.HDG, "123");

                Console.WriteLine("Press <ENTER> to set display text");
                Console.ReadLine();

                eventClient.SetDisplayText(Displays.GLARE.FCU.SPD, "---");

                Console.WriteLine("Press <ENTER> to disconnect from the SkalarkiIO Profiler");
                Console.ReadLine();

                eventClient.Disconnect();
            }
        }

        private static void OnHardwareEvent(IOEvent hardwareEvent, object state)
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
