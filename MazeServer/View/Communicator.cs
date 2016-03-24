using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Model;
using MazeServer.Interfaces;

namespace MazeServer.View
{
    class Communicator: Observable, IMazeView
    {
        int Port;
        byte[] ReceivedMessage;
        IPEndPoint Ipep;
        Socket ServerSock;
        RequestHandler Handler;

        public Communicator(int port)
        {
            this.Port = port;
            Ipep = new IPEndPoint(IPAddress.Any,Port);
            ServerSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Handler = new RequestHandler();
        }

        public void StartCommunication()
        {
            ServerSock.Bind(Ipep);
            ServerSock.Listen(10);

            Socket client = ServerSock.Accept();
            while (true)
            {
                ReceivedMessage = new byte[4096];
                int recv = ReceiveMessage(client, ReceivedMessage);
                if (recv == 0) break;

                NotifyObservers();
                //string message = Encoding.ASCII.GetString(ReceivedMessage, 0, recv);
                //SendMessage(client, message, recv);
            }
            client.Close();
        }

        public int ReceiveMessage(Socket client, byte[] data)
        {
            int recv = client.Receive(data);
            return recv;
        }

        public void SendMessage(Socket client, string message, int recv)
        {
            byte[] data = Encoding.ASCII.GetBytes(message.ToUpper());
            client.Send(data, recv, SocketFlags.None);
        }

        public string GetMessage()
        {
            return System.Text.Encoding.ASCII.GetString(ReceivedMessage);
        }
    }
}
