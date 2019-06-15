using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using Remoting;

namespace Server {
    class Program {
        static void Main(string[] args) {
            try {
                RemotingConfiguration.Configure("Server.exe.config", false);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
