using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Linq;
using Remoting;

namespace Client {
    class Program {
        static void Main(string[] args) {
            try {
                Console.Write("Create conn named: ");
                string _name = Console.ReadLine();

                RemotingConfiguration.Configure("Client.exe.config", false);

                var wko = new Remoting.WellKnownSingleton();
                ILease wko_leaseInfo = (ILease) wko.GetLifetimeService();
                wko_leaseInfo.Register(new SingletonSponsor(wko));

                var cao = new Remoting.ClientActivated(_name);
                ILease cao_leaseInfo = (ILease) cao.GetLifetimeService();
                cao_leaseInfo.Register(new ClientSponsor());

                wko.AddClient(_name);

                do {
                    try {
                        cao.Work();
                        Console.WriteLine("---   Singleton   ---");
                        Diagnostics.ShowLeaseInfo((ILease) wko.GetLifetimeService());
                        Console.WriteLine("---   Singleton   ---\n");
                        Console.WriteLine($"Connected clients:\n{string.Concat(wko.PrintConnectedClients().Select(s => s + "\n"))}");
                    } catch (Exception ex) {
                        Console.WriteLine($"Exception!\n{ex.Message}");
                    }

                } while (Console.ReadLine() != "q");

            } catch (Exception ex) {
                Console.WriteLine($"Exception!\n{ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
