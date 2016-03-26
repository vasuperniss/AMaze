using Maze_Library.Maze;
using System.Collections.Generic;
using Maze_Library.Maze.Matrix;

namespace Maze_Library
{
    class MatrixMaze : BaseMaze
    {
        private const char WALL = '1';
        private const char PASS = '0';

        private char[,] mazeMatrix;
        private int width;
        private int height;

        public MatrixMaze(int height, int width, IMazeWallBreaker wallBreaker)
        {
            this.mazeMatrix = new char[this.height = height * 2,
                                        this.width = width * 2];
            ReshapeAbleMatrixMaze reshapeAbleMaze = new ReshapeAbleMatrixMaze(this.mazeMatrix);
            reshapeAbleMaze.closeAllDoors();
            wallBreaker.BreakWalls(reshapeAbleMaze);
            this.mazeMatrix = reshapeAbleMaze.GetMazeMatrix();
        }

        public override List<IMazePosition> getAvailablePositionsFrom(IMazePosition position)
        {
            List<IMazePosition> result = new List<IMazePosition>();
            MatrixPosition pos = position as MatrixPosition;
            if (this.mazeMatrix[pos.Row, pos.Colomn] == WALL)
            {
                return result;
            }
            // upper cell
            if (pos.Row > 0)
            {
                if (this.mazeMatrix[pos.Row - 1, pos.Colomn] == PASS)
                {
                    result.Add(new MatrixPosition(pos.Row - 2, pos.Colomn));
                }
            }
            // lower cell 
            if (pos.Row < this.height - 1)
            {
                if (this.mazeMatrix[pos.Row + 1, pos.Colomn] == PASS)
                {
                    result.Add(new MatrixPosition(pos.Row + 2, pos.Colomn));
                }
            }
            // right cell
            if (pos.Colomn < this.width - 1)
            {
                if (this.mazeMatrix[pos.Row, pos.Colomn + 1] == PASS)
                {
                    result.Add(new MatrixPosition(pos.Row, pos.Colomn + 2));
                }
            }
            // left cell 
            if (pos.Colomn > 0)
            {
                if (this.mazeMatrix[pos.Row, pos.Colomn - 1] == PASS)
                {
                    result.Add(new MatrixPosition(pos.Row, pos.Colomn - 2));
                }
            }
            return result;
        }

        public override string ToString()
        {
            return this.mazeMatrix.ToString();
        }
    }
}
