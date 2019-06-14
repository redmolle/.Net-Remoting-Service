using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Lifetime;

public class Diagnostics
{
    public static void ShowLeaseInfo(ILease leaseInfo)
    {
        if (leaseInfo != null)
        {
            Console.WriteLine($"Current Lease time: {leaseInfo.CurrentLeaseTime.Minutes}:{leaseInfo.CurrentLeaseTime.Seconds}");

            Console.WriteLine($"Initial Lease time: {leaseInfo.InitialLeaseTime.Minutes}:{leaseInfo.InitialLeaseTime.Seconds}");

            Console.WriteLine($"Renew on call time: {leaseInfo.RenewOnCallTime.Minutes}:{leaseInfo.RenewOnCallTime.Seconds}");

            Console.WriteLine($"Current state: {leaseInfo.CurrentState}");
        }
        else
            Console.WriteLine("No Lease info");
    }
}
