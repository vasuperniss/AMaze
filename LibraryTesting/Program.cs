using System;
using Maze_Library.Maze;
using Maze_Library.Maze.WallBreakers;
using Maze_Library.Collections;
using System.Collections.Generic;

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

            Console.WriteLine("\n\nRandomized DFS Maze Solution is :");
            mazeDfs.SolveMaze(new MazeSolverFactory(WayToSolve.DFS));
            Console.WriteLine(mazeDfs.SolutionToString());

            Console.WriteLine("\n\nRandomized Prim Generated Maze :");
            IMaze mazePrim = mazeFact.GetMaze(randomPrimWBreakerFact);
            Console.WriteLine(mazePrim.ToString());

            //Console.WriteLine("\n\nRandomized Prim Maze Solution is :");
            //mazePrim.SolveMaze(new MazeSolverFactory(WayToSolve.BFS));
            //Console.WriteLine(mazeDfs.SolutionToString());

            PriorityQueue<int> pqueue = new PriorityQueue<int>(new IntComparer());
            pqueue.Queue(3);
            pqueue.Queue(5);
            pqueue.Queue(4);
            pqueue.Queue(2);
            pqueue.Queue(7);
            pqueue.Queue(3);
            pqueue.Queue(4);
            pqueue.Queue(4);
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());
            Console.WriteLine(pqueue.DeQueue());

            Console.ReadLine();
        }

        class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x > y)
                {
                    return 1;
                }
                else if (x == y)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
