using Maze_Library.Maze;
using MazeServer.View;

namespace MazeServer.Model
{
    /// <summary>
    /// delegate for notifying when the model is updated.
    /// </summary>
    /// <param name="sender">The sending object.</param>
    /// <param name="args">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
    delegate void UpdateModel(object sender, MessageEventArgs args);
    /// <summary>
    /// interface for a model.
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Represents an event that is raised when a task completes.
        /// </summary>
        event UpdateModel TaskCompleted;

        /// <summary>
        /// Adds a maze.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="maze">The maze.</param>
        void AddMaze(string name, IMaze maze);

        /// <summary>
        /// Adds a multiplayer game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="mp">The multiplayer game.</param>
        void AddMultiplayerGame(string name, MultiplayerGame mp);

        /// <summary>
        /// Adds a maze solution.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="jsonDesc">The json description of the solution.</param>
        void AddMazeSolution(string name, string jsonDesc);

        /// <summary>
        /// Determines whether a given client is in a multiplayer game(or waiting to play).
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns> the game if the client is in a game. 
        ///           null if he isn't. </returns>
        MultiplayerGame IsClientInGame(object client);

        /// <summary>
        /// Gets a maze.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <returns>the maze if it exists.
        ///          null if it doesn't. </returns>
        IMaze GetMaze(string name);

        /// <summary>
        /// Gets a multiplayer game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <returns>the game if it exists.
        ///          null if it doesn't. </returns>
        MultiplayerGame GetMultiplayerGame(string name);

        /// <summary>
        /// Removes a multiplayer game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        void RemoveMultiplayerGame(string name);

        /// <summary>
        /// Gets a maze solution.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <returns> the solution if it exists.
        ///           null if it doesn't. </returns>
        string GetMazeSolution(string name);

        /// <summary>
        /// Notifies the presenter that a task has been completed.
        /// </summary>
        /// <param name="from">the client that will receive an answer from the server.</param>
        /// <param name="reply">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        void CompletedTask(object from, MessageEventArgs reply);
    }
}
