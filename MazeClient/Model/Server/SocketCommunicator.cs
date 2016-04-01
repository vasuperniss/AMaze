using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MazeClient.Model.Server
{
    class SocketCommunicator
    {
        public delegate void toDo(string response);
        public event toDo OnResponse;

        private IPEndPoint serverIPEndPoint;
        private Socket server;
        private Thread listenerThread;

        public SocketCommunicator(string serverIp, int serverPort)
        {
            this.serverIPEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public bool EstablishConnection()
        {
            if (this.server.Connected) { return true; }
            if (this.listenerThread != null)
            {
                // close the thread safly
            }
            try
            {
                this.server.Connect(serverIPEndPoint);
                this.listenerThread = new Thread(this.ListenToResponses);
                this.listenerThread.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendRequest(string request)
        {
            if (this.server.Connected && request != null)
            {
                try
                {
                    int result = this.server.Send(Encoding.ASCII.GetBytes(request));
                    if (result == request.Length)
                    {
                        return true;
                    }
                }
                catch (Exception) { }
            }
            return false;
        }

        private void ListenToResponses()
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                try
                {
                    int len = this.server.Receive(buffer);
                    String response = Encoding.ASCII.GetString(buffer, 0, len);
                    this.OnResponse(response);
                }
                catch (SocketException)
                {
                    break;
                }
            }
        }

        public bool Close()
        {
            this.server.Close();
            this.listenerThread.Join();
            return true;
        }
    }
}
