using System;
using System.Net.Sockets;
using System.Text;

namespace MazeServer.View
{

    /// <summary>
    /// Handles communication with client.
    /// </summary>
    /// <seealso cref="MazeServer.View.IClientView" />
    class ClientHandler : IClientView
    {
        /// <summary>
        /// The client socket.
        /// </summary>
        private Socket ClientSocket;

        /// <summary>
        /// Occurs when a message has been received from the client.
        /// </summary>
        public event OnMessageReceived MessageReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="client">The client's socket.</param>
        public ClientHandler(Socket client)
        {
            ClientSocket = client;
        }

        /// <summary>
        /// Starts listening to messages from client. When a message is received the presenter is notified.
        /// </summary>
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

        /// <summary>
        /// Sends a message to the client.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            ClientSocket.Send(data);
        }
    }
}
