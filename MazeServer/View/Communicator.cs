using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MazeServer.Model;
using MazeServer.Presenter;

namespace MazeServer.View
{
    class Communicator: ILobbyView
    {
        private int port;
        private IPEndPoint ipep;
        private Socket serverSock;
        public event OnConnection OnConnect;

        public Communicator(int port)
        {
            this.port = port;
            ipep = new IPEndPoint(IPAddress.Any,port);
            serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSock.Bind(ipep);
        }

        public void StartListening()
        {
            serverSock.Listen(10);

            Socket client;
            while (true)
            {
                client = serverSock.Accept();
                ClientHandler ch = new ClientHandler(client);
                if(OnConnect != null) OnConnect(this, new ConnectionEventArgs(ch));
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
