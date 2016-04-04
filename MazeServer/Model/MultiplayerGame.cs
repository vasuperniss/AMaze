using System.Collections.Generic;
using Maze_Library.Maze;

namespace MazeServer.Model
{
    /// <summary>
    /// Multiplayer game.
    /// </summary>
    class MultiplayerGame
    {
        /// <summary>
        /// The clients list.
        /// </summary>
        private List<object> clients;
        /// <summary>
        /// The maze of the game.
        /// </summary>
        private IMaze maze;
        /// <summary>
        /// The name of the game.
        /// </summary>
        private string name;
        /// <summary>
        /// The model.
        /// </summary>
        private IModel model;
        /// <summary>
        /// The locking object.
        /// </summary>
        private object lockThis;

        /// <summary>
        /// indicates if a game had started.
        /// </summary>
        private bool gameStarted;

        /// <summary>
        /// Gets the number of clients.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return clients.Count; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplayerGame"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="maze">The maze used in the game.</param>
        public MultiplayerGame(IModel model, string name, IMaze maze)
        {
            this.name = name;
            this.maze = maze;
            this.model = model;
            lockThis = new object();
            clients = new List<object>();
            gameStarted = false;
        }

        /// <summary>
        /// Adds a client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns> 'true' if succeeded. 'false' if failed. </returns>
        public bool AddClient(object client)
        {
            lock (lockThis)
            {
                // check if client is not already in the game
                if (!clients.Contains(client) && clients.Count < 2)
                {
                    clients.Add(client);
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Starts the game.
        /// </summary>
        public void GameStarted()
        {
            gameStarted = true;
        }

        public bool IsInProgress()
        {
            return gameStarted;
        }

        /// <summary>
        /// Determines whether the specified clients list contains a given client.
        /// </summary>
        /// <param name="cl">The client.</param>
        /// <returns>'true' if the client is in this game. 'false' if he's not. </returns>
        public bool ContainsClient(object cl)
        {
            return clients.Contains(cl);
        }

        /// <summary>
        /// Retrieves the other client.
        /// </summary>
        /// <param name="cl">The client.</param>
        /// <param name="other">The other client.</param>
        public void RetrieveOtherClient(object cl, out object other)
        {
            other = null;
            foreach (object client in clients)
            {
                if (client != cl) other = client;
            }
        }

        /// <summary>
        /// Removes a client.
        /// </summary>
        /// <param name="cl">The client.</param>
        public void RemoveClient(object cl)
        {
            clients.Remove(cl);
        }

        /// <summary>
        /// Gets the name of the game.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Gets the maze used in the game.
        /// </summary>
        /// <returns></returns>
        public IMaze GetMaze()
        {
            return maze;
        }
    }
}
