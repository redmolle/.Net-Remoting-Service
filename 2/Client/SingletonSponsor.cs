using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

namespace Client
{
    public class SingletonSponsor: MarshalByRefObject, ISponsor
    {
        private int mRenewCount = 0;
        Remoting.WellKnownSingleton mManagedObj;

        public SingletonSponsor(Remoting.WellKnownSingleton managedObj)
        {
            mManagedObj = managedObj;
        }

        public TimeSpan Renewal(ILease leaseInfo)
        {
            Console.WriteLine("Client.SingletonSponsor.Renewal()");
            if(mRenewCount < 2)
            {
                mRenewCount++;
                return TimeSpan.FromSeconds(5);
            }
            else
            {
                if(mManagedObj.activeClientsCount == 0)
                {
                    ILease wko_leaseInfo = (ILease)mManagedObj.InitializeLifetimeService();
                    wko_leaseInfo.Register(new DisposeSingletonSponsor(mManagedObj));
                }
                return TimeSpan.FromSeconds(0);
            }
        }
    }
}
