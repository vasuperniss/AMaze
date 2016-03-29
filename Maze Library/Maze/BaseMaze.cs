using System.Collections.Generic;

namespace Maze_Library.Maze
{
    public abstract class BaseMaze : IMaze
    {
        protected MazePosition startPosition;
        protected MazePosition endPosition;

        public abstract List<MazePosition> GetAvailablePositionsFrom(MazePosition position);

        public abstract ISolution SolveMaze();

        public abstract string ToString(ISolution solution);

        public MazePosition GetFinishPosition()
        {
            return this.endPosition;
        }

        public MazePosition GetStartPosition()
        {
            return this.startPosition;
        }
    }
}
