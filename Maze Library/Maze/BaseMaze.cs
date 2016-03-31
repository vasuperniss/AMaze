using System;
using System.Collections.Generic;

namespace Maze_Library.Maze
{
    internal abstract class BaseMaze : IMaze
    {
        protected MazePosition startPosition;
        protected MazePosition endPosition;

        public abstract List<MazePosition> GetAvailablePositionsFrom(MazePosition position);

        public abstract void SolveMaze(MazeSolverFactory solver);

        public abstract string SolutionToString();

        public abstract void ChangeStartPosition();

        public abstract void ChangeEndPosition();

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
