using System;
using System.Collections.Generic;
using System.Linq;

namespace Remoting
{
    public class WellKnownSingleton : MarshalByRefObject, IDisposable
    {
        public int activeClientsCount { get { return clientNames == null ? 0 : clientNames.Count; } }
        private List<string> clientNames{ get; set; }

        public WellKnownSingleton()
        {
            Console.WriteLine("Remoting.Server.WellKnownSingleton()");
            clientNames = new List<string>();

        }

        public string[] PrintConnectedClients()
        {
            Console.WriteLine("Remoting.Server.PrintConnectedClients()");

            return clientNames.ToArray();
        }

        public void AddClient(string Name)
        {
            Console.WriteLine("Remoting.Server.AddClient()");

            clientNames.Add(Name);
        }

        public void DeleteClient(string Name)
        {
            Console.WriteLine("Remoting.Server.DeleteClient()");

            clientNames = clientNames.Where(w => w != Name).ToList();
        }

        public void Dispose()
        {
            Console.WriteLine("Remoting.Server.Dispose()");
            GC.SuppressFinalize(this);
        }
    }
}
