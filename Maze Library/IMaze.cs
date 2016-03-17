using System.Collections.Generic;

namespace Maze_Library
{
    public interface IMaze
    {
        MazePosition getStartPosition();

        MazePosition getFinishPosition();

        List<MazePosition> getAvailablePositionsFrom(MazePosition position);
    }
}
