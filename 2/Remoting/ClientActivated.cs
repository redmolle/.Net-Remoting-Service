using System;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Threading;

namespace Remoting {
    public class ClientActivated : MarshalByRefObject, IDisposable {
        public string Name { get; private set; }

        public ClientActivated(string name) {
            Console.WriteLine($"Remoting.ClientActivated(name = {name})");
            Name = name;
        }

        public override object InitializeLifetimeService() {
            Console.WriteLine("Remoting.ClientActivated.InitializeLifetimeService()");
            ILease leaseInfo = (ILease) base.InitializeLifetimeService();
            leaseInfo.Register(new DisposingSponsor(this));

            return leaseInfo;
        }

        public void Work() {
            Console.WriteLine($"\n---   ClientActivated {Name}   ---");
            Diagnostics.ShowLeaseInfo((ILease) this.GetLifetimeService());
            Console.WriteLine($"---   ClientActivated {Name}   ---\n");
        }

        public void Dispose() {
            Console.WriteLine($"Remoting.ClientActivated.Dispose() on thread {Thread.CurrentThread.GetHashCode()}");
            GC.SuppressFinalize(this);
        }

        ~ClientActivated() {
            Console.WriteLine($"CAO named {Name} was killed");
        }
    }
}
