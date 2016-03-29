using System.Collections.Generic;

namespace Maze_Library.Maze
{
    public abstract class BaseMaze : IMaze
    {
        protected MazePosition startPosition;
        protected MazePosition endPosition;

        public abstract List<MazePosition> getAvailablePositionsFrom(MazePosition position);

        public MazePosition getFinishPosition()
        {
            return this.endPosition;
        }

        public MazePosition getStartPosition()
        {
            return this.startPosition;
        }
    }
}
