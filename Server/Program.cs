using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using Remoting;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpChannel channel = new HttpChannel(13000);
            ChannelServices.RegisterChannel(channel);

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Remoting.Server.WellKnownSingleton),
                "MyURI.soap",
                WellKnownObjectMode.Singleton);

            RemotingConfiguration.RegisterWellKnownServiceType(
                
                typeof(
                )
            RemotingConfiguration.RegisterActivatedServiceType(typeof(Remoting.Client.ClientActivated));


            Console.ReadLine();

        }
    }
}
