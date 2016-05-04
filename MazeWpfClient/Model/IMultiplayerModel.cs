using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    /// <summary>
    /// Multiplayer model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IMultiPlayerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Loads the new game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        void LoadNewGame(string mazeName);

        /// <summary>
        /// Makes the move.
        /// </summary>
        /// <param name="move">The move.</param>
        void MakeMove(Move move);

        /// <summary>
        /// Solves the maze.
        /// </summary>
        void SolveMaze();

        /// <summary>
        /// Gets the hint.
        /// </summary>
        void GetHint();

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(string message);

        /// <summary>
        /// Gets or sets the name of the player maze.
        /// </summary>
        /// <value>
        /// The name of the player maze.
        /// </value>
        string PlayerMazeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the opponent maze.
        /// </summary>
        /// <value>
        /// The name of the opponent maze.
        /// </value>
        string OpponentMazeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        string GameName { get; set; }

        /// <summary>
        /// Gets or sets the player maze string.
        /// </summary>
        /// <value>
        /// The player maze string.
        /// </value>
        string PlayerMazeString { get; set; }

        /// <summary>
        /// Gets or sets the opponent maze string.
        /// </summary>
        /// <value>
        /// The opponent maze string.
        /// </value>
        string OpponentMazeString { get; set; }

        /// <summary>
        /// Gets or sets the solution string.
        /// </summary>
        /// <value>
        /// The solution string.
        /// </value>
        string SolutionString { get; set; }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        MazePosition PlayerPosition { get; set; }

        /// <summary>
        /// Gets or sets the opponent position.
        /// </summary>
        /// <value>
        /// The opponent position.
        /// </value>
        MazePosition OpponentPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player won game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [player won game]; otherwise, <c>false</c>.
        /// </value>
        bool PlayerWonGame { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the opponent won game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [opponent won game]; otherwise, <c>false</c>.
        /// </value>
        bool OpponentWonGame { get; set; }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>
        /// The hint.
        /// </value>
        MazePosition Hint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        bool isConnected { get; set; }
    }
}
