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
    class Communicator: ILobbyView
    {
        private int Port;
        private IPEndPoint Ipep;
        private Socket ServerSock;
        public event Update OnConnect;

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
                ClientHandler ch = new ClientHandler(client);
                OnConnect(this, new ConnectionEventArgs(ch));
                //ThreadPool.QueueUserWorkItem(state => new ClientHandler(client));
            }
        }

        public void StartListening()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
