using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

namespace Client
{
    public class ClientSponsor : MarshalByRefObject, ISponsor
    {
        private int mRenewCount = 0;
        
        public TimeSpan Renewal(ILease leaseInfo)
        {
            Console.WriteLine("Client.ClientSponsor.Renewal()");
            if(mRenewCount < 3)
            {
                mRenewCount++;
                return TimeSpan.FromSeconds(5);
            }
            else
            {
                return TimeSpan.FromSeconds(0);
            }
        }
    }
}
