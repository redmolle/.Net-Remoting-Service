using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;

namespace Remoting {
    public class WellKnownSingleton : MarshalByRefObject, IDisposable {
        public int activeClientsCount { get { return clientNames == null ? 0 : clientNames.Count; } }
        private List<string> clientNames { get; set; }

        public WellKnownSingleton() {
            Console.WriteLine("Remoting.WellKnownSingleton()");
            clientNames = new List<string>();

        }

        public string[] PrintConnectedClients() {
            Console.WriteLine("Remoting.WellKnownSingleton.PrintConnectedClients()");

            return clientNames.ToArray();
        }

        public void AddClient(string Name) {
            Console.WriteLine("Remoting.WellKnownSingleton.AddClient()");

            clientNames.Add(Name);
        }

        public void DeleteClient(string Name) {
            Console.WriteLine("Remoting.WellKnownSingleton.DeleteClient()");

            clientNames = clientNames.Where(w => w != Name).ToList();
        }

        public void Dispose() {
            Console.WriteLine("Remoting.WellKnownSingleton.Dispose()");
            GC.SuppressFinalize(this);
        }

        public override object InitializeLifetimeService() {
            Console.WriteLine("Remoting.WellKnownSingleton.InitializeLifetimeService()");
            ILease leaseInfo = (ILease) base.InitializeLifetimeService();
            //leaseInfo.Register(new SingletonServerSideSponsor(this));

            return leaseInfo;
        }

    }
}
