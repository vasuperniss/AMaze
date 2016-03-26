﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Library.Maze.WallBreakers
{
    class WallBreakerFactory
    {
        public enum BreakingType
        {
            DFS = 1, Random = 2
        }

        public IMazeWallBreaker GetWallBreaker(BreakingType type)
        {
            switch (type)
            {
                case BreakingType.DFS:
                    return new DFSWallBreaker();
                case BreakingType.Random:
                    return new RandomWallBreaker();
                default:
                    return null;
            }
        }
    }
}
