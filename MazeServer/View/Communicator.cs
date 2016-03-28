using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MazeServer.Model;
using MazeServer.Interfaces;
using MazeServer.Presenter;

namespace MazeServer.View
{
    class Communicator
    {
        private int Port;
        private IPEndPoint Ipep;
        private Socket ServerSock;
        event Update Notify;

        public Communicator(int port)
        {
            this.Port = port;
            Ipep = new IPEndPoint(IPAddress.Any,Port);
            ServerSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartCommunication()
        {
            ServerSock.Bind(Ipep);
            ServerSock.Listen(10);

            Socket client;
            while (true)
            {
                client = ServerSock.Accept();
                ThreadPool.QueueUserWorkItem(state => new ClientHandler(client));
            }
        }
    }
}
