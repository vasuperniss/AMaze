using System.Net;
using System.Net.Sockets;

namespace MazeServer.View
{

    /// <summary>
    /// Handles connections from clients.
    /// </summary>
    /// <seealso cref="MazeServer.View.ILobbyView" />
    class Communicator : ILobbyView
    {
        /// <summary>
        /// The port
        /// </summary>
        private int port;

        /// <summary>
        /// The IP end point.
        /// </summary>
        private IPEndPoint ipep;

        /// <summary>
        /// The server socket.
        /// </summary>
        private Socket serverSock;

        /// <summary>
        /// Occurs when a client connects.
        /// </summary>
        public event OnConnection OnConnect;

        /// <summary>
        /// Initializes a new instance of the <see cref="Communicator"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        public Communicator(int port)
        {
            this.port = port;
            ipep = new IPEndPoint(IPAddress.Any,port);
            serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSock.Bind(ipep);
        }

        /// <summary>
        /// Starts listening. When a client is connected a new ClientHandler is created for him and the presenter is notified.
        /// </summary>
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
    }
}
