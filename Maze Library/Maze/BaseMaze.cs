using System;
using System.Collections.Generic;

namespace Maze_Library.Maze
{
    public abstract class BaseMaze : IMaze
    {
        protected MazePosition startPosition;
        protected MazePosition endPosition;

        public abstract List<MazePosition> GetAvailablePositionsFrom(MazePosition position);

        public abstract void SolveMaze(MazeSolverFactory solver);

        public abstract string SolutionToString();

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
