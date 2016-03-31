using Maze_Library.Maze.Matrix;

namespace Maze_Library.Maze
{
    class MazeFactory
    {
        private int colomns;
        private int rows;

        public MazeFactory(int rows, int colomns)
        {
            this.rows = rows;
            this.colomns = colomns;
        }

        public IMaze GetMaze(WallBreakers.WallBreakerFactory wBreakerFactory)
        {
            return new MatrixMaze(this.rows, this.colomns, wBreakerFactory);
        }

        public IMaze CopyMaze(IMaze maze)
        {
            if (maze is MatrixMaze)
            {
                return new MatrixMaze(maze as MatrixMaze);
            }
            return null;
        }
    }
}
