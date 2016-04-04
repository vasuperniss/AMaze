using Maze_Library.Maze;
using MazeServer.Model;
using System;
using System.Net.Sockets;
using System.Text;

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
            byte[] data = new byte[4096];
            while (true)
            {
                try
                {
                    int recv = ClientSocket.Receive(data);
                    if (recv == 0) break;

                    string message = Encoding.ASCII.GetString(data, 0, recv);

                    if (MessageReceived != null) MessageReceived(this, new MessageEventArgs(message));
                }
                catch (Exception)
                {
                    break;
                }
            }
            ClientSocket.Close();
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            ClientSocket.Send(data);
        }
    }
}
