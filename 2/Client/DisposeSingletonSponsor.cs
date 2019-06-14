using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

namespace Client
{
    class DisposeSingletonSponsor : MarshalByRefObject, ISponsor
    {
        private IDisposable mManagedObj;
        public DisposeSingletonSponsor(IDisposable managedObj)
        {
            Console.WriteLine("Client.DisposeSingletonSponsor()");
            mManagedObj = managedObj;
        }

        public TimeSpan Renewal(ILease leaseInfo)
        {
            Console.WriteLine("Client.DisposeSingletonSponsor.Renewal()");
            mManagedObj.Dispose();
            return TimeSpan.FromSeconds(0);
        }
    }
}
