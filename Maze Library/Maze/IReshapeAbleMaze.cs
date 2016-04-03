/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// Represents a Maze that can be reshaped
    /// </summary>
    /// <seealso cref="Maze_Library.Maze.IMaze" />
    internal interface IReshapeAbleMaze : IMaze
    {
        /// <summary>
        /// Opens all doors in the Maze.
        /// </summary>
        void OpenAllDoors();

        /// <summary>
        /// Closes all doors in the Maze.
        /// </summary>
        void CloseAllDoors();

        /// <summary>
        /// Changes the door state between first and second.
        /// </summary>
        /// <param name="first">The first MazePosition.</param>
        /// <param name="second">The second MazePosition.</param>
        /// <param name="state">The door state to be set.</param>
        void ChangeDoorStateBetween(MazePosition first, MazePosition second,
                                                            DoorState state);

        /// <summary>
        /// Changes the end position.
        /// </summary>
        /// <param name="position">The new End position.</param>
        /// <returns>true if successfuly changed the position</returns>
        bool ChangeEndPosition(MazePosition position);
    }
}
