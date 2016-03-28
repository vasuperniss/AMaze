using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    class ClientHandler: IMazeView
    {
        private Socket ClientSocket;
        private byte[] ReceivedMessage;
        private int Recv;
        public event Update ViewChanged;

        public ClientHandler(Socket client)
        {
            ClientSocket = client;
            EstablishConnection();
        }

        private void EstablishConnection()
        {
            while (true)
            {
                ReceivedMessage = new byte[4096];
                Recv = ReceiveMessage(ReceivedMessage);
                if (Recv == 0) break;

                ViewChanged(this, EventArgs.Empty);

                string message = Encoding.ASCII.GetString(ReceivedMessage, 0, Recv);
                Console.WriteLine(message);
                //SendMessage(client, message, recv);
            }
            ClientSocket.Close();
        }

        public int ReceiveMessage(byte[] data)
        {
            int recv = ClientSocket.Receive(data);
            return recv;
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message.ToUpper());
            ClientSocket.Send(data, Recv, SocketFlags.None);
        }

        public string GetMessage()
        {
            return Encoding.ASCII.GetString(ReceivedMessage);
        }

        public void SendReply(string reply)
        {
            SendMessage(reply);
        }
    }
}
