using System.Collections.Generic;

namespace Maze_Library.Maze
{
    public interface IMaze
    {
        MazePosition getStartPosition();

        MazePosition getFinishPosition();

        List<MazePosition> getAvailablePositionsFrom(MazePosition position);
    }
}
