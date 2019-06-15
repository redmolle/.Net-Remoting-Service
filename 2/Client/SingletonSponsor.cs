using System;
using System.Runtime.Remoting.Lifetime;

namespace Client {
    public class SingletonSponsor : MarshalByRefObject, ISponsor {
        private int mRenewCount = 0;
        Remoting.WellKnownSingleton mManagedObj;

        public SingletonSponsor(Remoting.WellKnownSingleton managedObj) {
            mManagedObj = managedObj;
        }

        public TimeSpan Renewal(ILease leaseInfo) {
            Console.WriteLine("Client.SingletonSponsor.Renewal()");
            if (mRenewCount < 1) {
                mRenewCount++;
                return leaseInfo.RenewOnCallTime;
            } else {
                mManagedObj.Dispose();
                return TimeSpan.FromSeconds(0);
            }
        }
    }
}
