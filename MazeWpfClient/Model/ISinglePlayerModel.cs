using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    /// <summary>
    /// Movement Direction enum
    /// </summary>
    public enum Move { Up, Down,Left,Right }

    /// <summary>
    /// Single player model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface ISinglePlayerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Loads a new game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        void LoadNewGame(string mazeName);
        /// <summary>
        /// Makes the move on the Maze.
        /// </summary>
        /// <param name="move">The move.</param>
        void MakeMove(Move move);
        /// <summary>
        /// Solves the maze.
        /// </summary>
        void SolveMaze();
        /// <summary>
        /// Gets a hint.
        /// </summary>
        void GetHint();

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        string MazeName { get; set; }
        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>
        /// The maze string.
        /// </value>
        string MazeString { get; set; }
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
        /// Gets or sets a value indicating whether [won game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [won game]; otherwise, <c>false</c>.
        /// </value>
        bool WonGame { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [lost game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [lost game]; otherwise, <c>false</c>.
        /// </value>
        bool LostGame { get; set; }
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

        /// <summary>
        /// Restarts the maze.
        /// </summary>
        void Restart();
    }
}
