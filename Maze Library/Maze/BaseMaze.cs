using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Library.Maze
{
    public abstract class BaseMaze : IMaze
    {
        protected IMazePosition startPosition;
        protected IMazePosition endPosition;

        public abstract List<IMazePosition> getAvailablePositionsFrom(IMazePosition position);

        public IMazePosition getFinishPosition()
        {
            return this.endPosition;
        }

        public IMazePosition getStartPosition()
        {
            return this.startPosition;
        }
    }
}
