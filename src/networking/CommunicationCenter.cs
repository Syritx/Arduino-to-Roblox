using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Threading.Tasks;
using System.Text;

using System.Diagnostics;

namespace test {

    class CommunicationCenter {
        
        Socket ServerSocket;
        IPAddress iP;
        int Port;
        Process External;

        public CommunicationCenter(IPAddress iP, int Port) {
            this.iP = iP;
            this.Port = Port;
        }

        public void Start() {
            ServerSocket = new Socket(AddressFamily.InterNetwork, 
                                      SocketType.Stream, 
                                      ProtocolType.Tcp);

            IPEndPoint endPoint = new IPEndPoint(iP, Port);
            ServerSocket.Bind(endPoint);
            ServerSocket.Listen(5);

            WaitForClients();
        }

        void WaitForClients() {
            while (true) {
                Console.WriteLine("Waiting for clients");

                Socket client = ServerSocket.Accept();
                Console.WriteLine("Connection from: {0}", ((IPEndPoint)client.RemoteEndPoint).Address);
                Task.Run(() => ClientThread(client));
            }
        }

        // -------------- //
        // CLIENT THREAD
        // -------------- //

        void ClientThread(Socket client) {

            do {
                byte[] bytes = new byte[2048];
                client.Receive(bytes);

                string command = Encoding.UTF8.GetString(bytes);
                Console.WriteLine("Command: " + command);

                if (command.StartsWith("in bounds")) {

                    File.Delete("src/flaskapp/format.json");
                    
                    string jsonFormat = "{ \"hovered\": true }";
                    TextWriter writer = new StreamWriter("src/flaskapp/format.json", true);
                    writer.Write(jsonFormat);
                    writer.Close();
                }

                if (command.StartsWith("out bounds")) {
                    File.Delete("src/flaskapp/format.json");
                    
                    string jsonFormat = "{ \"hovered\": false }";
                    TextWriter writer = new StreamWriter("src/flaskapp/format.json", true);
                    writer.Write(jsonFormat);
                    writer.Close();
                }
            }
            while (true);
        }
    }
}