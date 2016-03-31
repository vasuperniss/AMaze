using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    class ClientHandler: IClientView
    {
        private Socket ClientSocket;
        public event OnMessageReceived MessageReceived;

        public ClientHandler(Socket client)
        {
            ClientSocket = client;
        }

        public void StartListening()
        {
            while (true)
            {
                byte[] data = new byte[4096];
                int recv = ClientSocket.Receive(data);
                if (recv == 0) break;

                string message = Encoding.ASCII.GetString(data, 0, recv);
                if (MessageReceived != null) MessageReceived(this, new MessageEventArgs(message));
 
                Console.WriteLine(message);
                //SendMessage(message, recv);
            }
            ClientSocket.Close();
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message.ToUpper());
            ClientSocket.Send(data);
        }
    }
}
