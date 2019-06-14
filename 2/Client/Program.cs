using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using Remoting;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Create conn named: ");
                string _name = Console.ReadLine();

                RemotingConfiguration.Configure("Client.exe.config", false);

                var cao = new Remoting.ClientActivated(_name);
                ILease cao_leaseInfo = (ILease)cao.InitializeLifetimeService();
                cao_leaseInfo.Register(new ClientSponsor());
                




                var wko = new Remoting.WellKnownSingleton();
                ILease wko_leaseInfo = (ILease)wko.InitializeLifetimeService();
                wko_leaseInfo.Register(new SingletonSponsor(wko));
                do
                {
                    cao.Work();
                    Console.WriteLine("Connected clients:\n");
                    Console.WriteLine(string.Concat(wko.PrintConnectedClients().Select(s => s + "\n")));
                } while (Console.ReadLine() != "q");



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception!\n{ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
