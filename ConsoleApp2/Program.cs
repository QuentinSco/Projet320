
namespace FAQU
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using SkalarkiIO.SDK;

    class Program
    {
        static FSUIPCHandler fsuipcHandler;     // FSUIPC object
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
            FAQUBrickOverhead faqu_overhead = new FAQUBrickOverhead();

            IPAddress localIP = GetLocalIPAddress();
            fsuipcHandler = new FSUIPCHandler();

            using (eventClient = new EventClient(localIP, 53000, (e, s) => {
                fAQU.OnHardwareEvent(e, s);
                faqu_overhead.OnHardwareEvent(e, s);
            }, null))
            {
                eventClient.ConnectedDevicesChanged += (s, a) =>
                {
                    Console.WriteLine("Hardware {0} has {1}", a.Device, a.Connected ? "connected" : "disconnected");
                };

                faqu_overhead.Setup(eventClient, fsuipcHandler);
                fAQU.Setup(eventClient, fsuipcHandler);

                eventClient.ConnectAsync().Wait();

                Console.ReadLine();
                eventClient.Disconnect();
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