using System.Collections.Generic;

namespace Maze_Library.Maze
{
    public enum DoorState
    {
        Closed, Opened
    }

    public interface IMaze
    {
        MazePosition GetStartPosition();

        MazePosition GetFinishPosition();

        List<MazePosition> GetAvailablePositionsFrom(MazePosition position);

        void SolveMaze(MazeSolverFactory solver);

        string SolutionToString();
    }
}
