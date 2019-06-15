using System;
using System.Threading;
using System.Runtime.Remoting.Lifetime;
using System.Linq;

namespace Remoting
{
    public class ClientActivated : MarshalByRefObject, IDisposable
    {
        public string Name { get; private set; }
        private WellKnownSingleton wko;

        public ClientActivated(string name)
        {
            Console.WriteLine($"Remoting.ClientActivated(name = {name})");
            Name = name;
            wko = (WellKnownSingleton)Activator
                .GetObject(
                typeof(WellKnownSingleton),
                "http://localhost:13000/MyURITON.soap");

            wko.AddClient(Name);
        }

        public override object InitializeLifetimeService()
        {
            Console.WriteLine("Remoting.ClientActivated.InitializeLifetimeService()");
            ILease leaseInfo = (ILease)base.InitializeLifetimeService();
            leaseInfo.Register(new DisposingSponsor(this));

            return leaseInfo;
        }

        public string Work()
        {
            Console.WriteLine($"\n---   ClientActivated {Name}   ---");
            Diagnostics.ShowLeaseInfo((ILease)this.GetLifetimeService());
            Console.WriteLine($"---   ClientActivated {Name}   ---\n");


            return string.Concat(wko.PrintConnectedClients().Select(s => s + "\n"));
        }

        public void Dispose()
        {
            Console.WriteLine($"Remoting.ClientActivated.Dispose() on thread {Thread.CurrentThread.GetHashCode()}");
            wko = (WellKnownSingleton)Activator
                .GetObject(
                typeof(WellKnownSingleton),
                "http://localhost:13000/MyURITON.soap");
            wko.DeleteClient(Name);
            GC.SuppressFinalize(this);
        }

        ~ClientActivated()
        {
            Console.WriteLine($"CAO named {Name} was killed");
        }
    }
}
