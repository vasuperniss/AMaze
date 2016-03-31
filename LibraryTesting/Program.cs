using System;
using Maze_Library.Maze;
using Maze_Library.Maze.WallBreakers;

namespace LibraryTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            MazeFactory mazeFact = new MazeFactory(18, 30);

            MazeSolverFactory bfsSolverFact = new MazeSolverFactory(WayToSolve.BFS);
            MazeSolverFactory dfsSolverFact = new MazeSolverFactory(WayToSolve.BFS);

            WallBreakerFactory dfsWBreakerFact
                        = new WallBreakerFactory(WallBreakerFactory.BreakingType.DFS);
            WallBreakerFactory randomPrimWBreakerFact
                        = new WallBreakerFactory(WallBreakerFactory.BreakingType.Random);

            Console.WriteLine("Randomized DFS Generated Maze :");
            IMaze mazeDfs = mazeFact.GetMaze(dfsWBreakerFact);
            Console.WriteLine(mazeDfs.ToString());

            Console.WriteLine("\n\nRandomized Prim Generated Maze :");
            IMaze mazePrim = mazeFact.GetMaze(randomPrimWBreakerFact);
            Console.WriteLine(mazePrim.ToString());

            Console.ReadLine();
        }
    }
}
