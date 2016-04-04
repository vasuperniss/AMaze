
namespace MazeServer.View
{
    /// <summary>
    /// delegate for handling messages from client.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <param name="args">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
    public delegate void OnMessageReceived(object o, MessageEventArgs args);

    /// <summary>
    /// Interface for a client. Contains a delegate to notify listeners when a message has been received from the client.
    /// </summary>
    public interface IClientView
    {
        /// <summary>
        /// Occurs when a message has been received from the client.
        /// </summary>
        event OnMessageReceived MessageReceived;

        /// <summary>
        /// Starts listening.
        /// </summary>
        void StartListening();

        /// <summary>
        /// Sends a message to the client.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(string message);
    }
}
