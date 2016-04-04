using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MazeClient.Model.Server
{
    /// <summary>
    /// a Socket Communicator
    /// </summary>
    class SocketCommunicator
    {
        /// <summary>
        /// delegate for func(string) functions
        /// </summary>
        /// <param name="response">The response.</param>
        public delegate void toDo(string response);
        /// <summary>
        /// Occurs when [on response].
        /// </summary>
        public event toDo OnResponse;

        /// <summary>
        /// The server ip end point
        /// </summary>
        private IPEndPoint serverIPEndPoint;
        /// <summary>
        /// The server Socket
        /// </summary>
        private Socket server;
        /// <summary>
        /// The listener thread for listening to the Server's Responses
        /// </summary>
        private Thread listenerThread;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketCommunicator"/> class.
        /// </summary>
        /// <param name="serverIp">The server ip.</param>
        /// <param name="serverPort">The server port.</param>
        public SocketCommunicator(string serverIp, int serverPort)
        {
            this.serverIPEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Establishes the connection with the Server.
        /// And in addition starts listening to the server responses.
        /// </summary>
        /// <returns>true if successfuly connected to the server</returns>
        public bool EstablishConnection()
        {
            if (this.server.Connected) { return true; }
            if (this.listenerThread != null)
            {
                return false;
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

        /// <summary>
        /// Sends the request to the Server.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>true if successful</returns>
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

        /// <summary>
        /// Listens to responses from the server.
        /// </summary>
        private void ListenToResponses()
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                try
                {
                    int len = this.server.Receive(buffer);
                    if (len == 0)
                    {
                        // server closed
                        this.OnResponse(null);
                        break;
                    }
                    String response = Encoding.ASCII.GetString(buffer, 0, len);
                    this.OnResponse(response);
                }
                catch (SocketException)
                {
                    // server closed
                    this.OnResponse(null);
                    break;
                }
            }
        }

        /// <summary>
        /// Closes the connection with the server and stops listenning.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            this.server.Close();
            this.listenerThread.Join();
            return true;
        }
    }
}
