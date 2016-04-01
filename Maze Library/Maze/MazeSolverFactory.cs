using Maze_Library.Algorithms;
using System.Collections.Generic;

namespace Maze_Library.Maze
{
    public enum WayToSolve
    {
        DFS = 0, BFS = 1
    }

    public class MazeSolverFactory
    {
        private WayToSolve way;

        public MazeSolverFactory(WayToSolve wayToSolve)
        {
            this.way = wayToSolve;
        }

        internal List<MazePosition> SolveMaze(IMaze maze)
        {
            List<MazePosition> solution = new List<MazePosition>();
            PathSearchResult<MazePosition> result;
            switch (this.way)
            {
                case WayToSolve.BFS:
                    result = new BFS<MazePosition>().Search(new SearchableMaze(maze));
                    break;
                case WayToSolve.DFS:
                    result = new DFS<MazePosition>().Search(new SearchableMaze(maze));
                    break;
                default:
                    return null;
            }
            for (int i = 0; i < result.GetPathLenght(); i++)
            {
                solution.Add(result[i].TState);
            }
            return solution;
        }
    }
}
