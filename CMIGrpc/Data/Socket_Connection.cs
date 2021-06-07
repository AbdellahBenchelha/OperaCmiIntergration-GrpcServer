using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CMIGrpc.Data
{
    public class Socket_Connection
    {
        private static byte[] _buffer = new byte[1024];
        private static List<Socket> _clientSockets = new List<Socket>();
        private static Socket _serverSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
            );

        private static Socket serverSocket=null, clientSocket;
        private static byte[] buffer;


        public static string Payment(string TAG_AMOUNT,string TAG_CURRENCY="504")
        {
            // Create server socket and listen on any local interface.
            if (serverSocket==null)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
                serverSocket.Listen(0);

                // Followed by periods to indicate work.
                Console.WriteLine("Listening for connections on port 3333...");
                clientSocket = serverSocket.Accept();
                Console.WriteLine("Client Connected");
            }
            

            string AMOUNT = SplitSocketData.ConvertMadToCentimes(TAG_AMOUNT);
            string input = "#P|MN"+ AMOUNT + "|CR"+ TAG_CURRENCY + "|";
            clientSocket.Send(Encoding.ASCII.GetBytes(input));

            Console.WriteLine("Waiting for data...");
            buffer = new byte[clientSocket.ReceiveBufferSize];
            string receivedMsg=ReceiveData();
            return receivedMsg;
            //serverSocket.Close();
            //Console.WriteLine("Server loop ended. Press enter to exit.");
            //Console.ReadLine();
            //serverSocket.Close();
        }
        public static string preauthorisation(string TAG_AMOUNT, string TAG_CURRENCY = "504")
        {
            // Create server socket and listen on any local interface.
            if (serverSocket == null)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
                serverSocket.Listen(0);

                // Followed by periods to indicate work.
                Console.WriteLine("Listening for connections on port 3333...");
                clientSocket = serverSocket.Accept();
                Console.WriteLine("Client Connected");
            }


            string AMOUNT = SplitSocketData.ConvertMadToCentimes(TAG_AMOUNT);
            string input = "#A|MN" + AMOUNT + "|CR" + TAG_CURRENCY + "|";
            clientSocket.Send(Encoding.ASCII.GetBytes(input));

            Console.WriteLine("Waiting for data...");
            buffer = new byte[clientSocket.ReceiveBufferSize];
            string receivedMsg = ReceiveData();
            return receivedMsg;
            //serverSocket.Close();
            //Console.WriteLine("Server loop ended. Press enter to exit.");
            //Console.ReadLine();
            //serverSocket.Close();
        }
        public static string preauthorisation_confirmayion(string TAG_AMOUNT, string TAG_CURRENCY = "504")
        {
            // Create server socket and listen on any local interface.
            if (serverSocket == null)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
                serverSocket.Listen(0);

                // Followed by periods to indicate work.
                Console.WriteLine("Listening for connections on port 3333...");
                clientSocket = serverSocket.Accept();
                Console.WriteLine("Client Connected");
            }


            string AMOUNT = SplitSocketData.ConvertMadToCentimes(TAG_AMOUNT);
            string input = "#W|MN" + AMOUNT + "|CR" + TAG_CURRENCY + "|";
            clientSocket.Send(Encoding.ASCII.GetBytes(input));

            Console.WriteLine("Waiting for data...");
            buffer = new byte[clientSocket.ReceiveBufferSize];
            string receivedMsg = ReceiveData();
            return receivedMsg;
            //serverSocket.Close();
            //Console.WriteLine("Server loop ended. Press enter to exit.");
            //Console.ReadLine();
            //serverSocket.Close();
        }
        public static string preauthorisation_avoid(string TAG_AMOUNT, string TAG_CURRENCY = "504")
        {
            // Create server socket and listen on any local interface.
            if (serverSocket == null)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3333));
                serverSocket.Listen(0);

                // Followed by periods to indicate work.
                Console.WriteLine("Listening for connections on port 3333...");
                clientSocket = serverSocket.Accept();
                Console.WriteLine("Client Connected");
            }


          
            string input = "#C|MN";
            clientSocket.Send(Encoding.ASCII.GetBytes(input));

            Console.WriteLine("Waiting for data...");
            buffer = new byte[clientSocket.ReceiveBufferSize];
            string receivedMsg = ReceiveData();
            return receivedMsg;
            //serverSocket.Close();
            //Console.WriteLine("Server loop ended. Press enter to exit.");
            //Console.ReadLine();
            //serverSocket.Close();
        }



        private static string ReceiveData()
        {
            while (clientSocket.Connected)
            {
                int received = clientSocket.Receive(buffer);

                if (received == 0) // Assume the client has disconnected.
                {
                    Console.WriteLine("Client Disconnected");
                    break;
                }

                // Shrink the buffer so we don't get null chars in the text.
                Array.Resize(ref buffer, received);
                string receivedMsg = Encoding.ASCII.GetString(buffer);
                // Reset the buffer.
                Array.Resize(ref buffer, clientSocket.ReceiveBufferSize);
                Console.WriteLine("Message received: " + receivedMsg);

                return receivedMsg;
               // Console.Write("Enter text to send: ");
                //string input = Console.ReadLine();
                //clientSocket.Send(Encoding.ASCII.GetBytes(input));
            }

            // Assume the client has disconnected and start listening again for connections.
            Console.WriteLine("\nListening again...");
            clientSocket = serverSocket.Accept();
            Console.WriteLine("Client Connected");
            Console.WriteLine("Waiting for data...");
            ReceiveData();
            return null;
        }

    }
}
