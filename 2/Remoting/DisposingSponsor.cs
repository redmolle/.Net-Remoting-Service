﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

namespace Remoting
{
    class DisposingSponsor : ISponsor
    {
        private IDisposable mManagedObj;
        public DisposingSponsor(IDisposable managedObj)
        {
            Console.WriteLine("Remoting.DisposingSponsor()");
            mManagedObj = managedObj;
        }

        public TimeSpan Renewal(ILease leaseInfo)
        {
            Console.WriteLine("Remoting.DisposingSponsor.Renewal()");
            mManagedObj.Dispose();
            return TimeSpan.FromSeconds(0);
        }
    }
}
