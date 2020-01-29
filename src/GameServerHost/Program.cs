using System;
using System.ServiceModel;
using GameServerWCF;

namespace GameServerHost
{
    internal class Program
    {
        private static void Main(String[] args)
        {
            using (var host = new ServiceHost(typeof(CGameChoiceService)))
            {
                host.Opened += HostOnOpened;
                host.Open();
                Console.Read();
            }
        }

        private static void HostOnOpened(Object sender, EventArgs e)
        {
            Console.WriteLine("Host started");
            if (!(sender is ServiceHost host)) return;

            foreach (Uri hostBaseAddress in host.BaseAddresses)
            {
                Console.WriteLine($"Available on {hostBaseAddress.AbsoluteUri}");
            }
        }
    }
}