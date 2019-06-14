using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

namespace Remoting
{
    class SingletonServerSideSponsor : ISponsor
    {
        private int mRenewCount = 0;
        Remoting.WellKnownSingleton mManagedObj;

        public SingletonServerSideSponsor(Remoting.WellKnownSingleton managedObj)
        {
            mManagedObj = managedObj;
        }

        public TimeSpan Renewal(ILease leaseInfo)
        {
            Console.WriteLine("Remoting.SingletonServerSideSponsor.Renewal()");
            if (mRenewCount < 2)
            {
                mRenewCount++;
                return TimeSpan.FromSeconds(5);
            }
            else
            {
                mManagedObj.Dispose();
                return TimeSpan.FromSeconds(0);
            }
        }
    }
}
