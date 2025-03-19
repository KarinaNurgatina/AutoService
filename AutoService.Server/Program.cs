using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Program
{
    static void Main()
    {
        TcpChannel channel = new TcpChannel(8086);
        ChannelServices.RegisterChannel(channel, false);

        RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(RequestManager),
            "RequestManager",
            WellKnownObjectMode.Singleton
        );

        Console.WriteLine("Сервер запущен...");
        Console.ReadLine();
    }
}
