
using System;
using System.Net;

namespace test
{
    class Program
    {
        static int Port;
        static IPAddress iPAddress;
        static CommunicationCenter Server;

        public static void Main(string[] args) {
            try {
                Port = Int32.Parse(args[1]);
                iPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

                Server = new CommunicationCenter(iPAddress, Port);
                Server.Start();
            }
            catch (Exception e) {Console.WriteLine(e.StackTrace);}
        }
    }
}
