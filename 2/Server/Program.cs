using System;
using System.Runtime.Remoting;

namespace Server {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Remoting server is running\nPress q to exit");
            try {
                RemotingConfiguration.Configure("Server.exe.config", false);
                while (Console.ReadLine() != "q");
            } catch (Exception ex) {
                Console.WriteLine($"Exception!\n{ex.Message}");
            }
        }
    }
}
