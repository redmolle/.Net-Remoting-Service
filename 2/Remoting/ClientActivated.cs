using System;
using System.Threading;
using System.Runtime.Remoting.Lifetime;
using System.Linq;

namespace Remoting
{
    public class ClientActivated : MarshalByRefObject, IDisposable
    {
        public string Name { get; private set; }
        private WellKnownSingleton wko = new WellKnownSingleton();

        public ClientActivated(string name)
        {
            Console.WriteLine($"Remoting.ClientActivated(name = {name})");
            Name = name;
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
            Console.WriteLine("---   ClientActivated   ---");
            Diagnostics.ShowLeaseInfo((ILease)this.GetLifetimeService());
            Console.WriteLine("---   ClientActivated   ---\n");


            return string.Concat(wko.PrintConnectedClients().Select(s => s + "\n"));
        }

        public void Dispose()
        {
            Console.WriteLine($"Remoting.ClientActivated.Dispose() on thread {Thread.CurrentThread.GetHashCode()}");
            wko.DeleteClient(Name);
            GC.SuppressFinalize(this);
        }

        ~ClientActivated()
        {
            Console.WriteLine($"CAO named {Name} was killed");
        }
    }
}
