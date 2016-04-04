
namespace MazeServer.View
{

    /// <summary>
    /// delegate for notifying listeners when a client connects.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <param name="args">The <see cref="ConnectionEventArgs"/> instance containing the event data.</param>
    public delegate void OnConnection(object o, ConnectionEventArgs args);

    /// <summary>
    /// Interface for a class that handles connection of clients to the server.
    /// </summary>
    interface ILobbyView
    {

        /// <summary>
        /// Occurs when a client connects.
        /// </summary>
        event OnConnection OnConnect;

        /// <summary>
        /// Starts listening.
        /// </summary>
        void StartListening();
    }
}
