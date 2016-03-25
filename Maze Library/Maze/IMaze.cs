using System.Collections.Generic;

namespace Maze_Library
{
    public interface IMaze
    {
        IMazePosition getStartPosition();

        IMazePosition getFinishPosition();

        List<IMazePosition> getAvailablePositionsFrom(IMazePosition position);
    }
}
