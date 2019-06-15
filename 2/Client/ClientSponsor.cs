using System;
using System.Runtime.Remoting.Lifetime;

namespace Client {
    public class ClientSponsor : MarshalByRefObject, ISponsor {
        private int mRenewCount = 0;

        public TimeSpan Renewal(ILease leaseInfo) {
            Console.WriteLine("Client.ClientSponsor.Renewal()");
            if (mRenewCount < 2) {
                mRenewCount++;
                return leaseInfo.RenewOnCallTime;
            } else {
                return TimeSpan.FromSeconds(0);
            }
        }
    }
}
