using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

namespace Client
{
    public class SingletonClientSideSponsor: MarshalByRefObject, ISponsor
    {
        private int mRenewCount = 0;
        Remoting.WellKnownSingleton mManagedObj;

        public SingletonClientSideSponsor(Remoting.WellKnownSingleton managedObj)
        {
            mManagedObj = managedObj;
        }

        public TimeSpan Renewal(ILease leaseInfo)
        {
            Console.WriteLine("Client.SingletonClientSideSponsor.Renewal()");
            if(mRenewCount < 2)
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
