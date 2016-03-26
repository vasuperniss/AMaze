using System.Collections.Generic;
using Maze_Library.Maze;
using Maze_Library.Maze.Matrix;

namespace Maze_Library
{
    public class ReshapeAbleMatrixMaze : BaseMaze, IReshapeAbleMaze
    {
        private const char WALL = '1';
        private const char PASS = '0';

        private char[,] mazeMatrix;
        private int height, width;

        public ReshapeAbleMatrixMaze(char[,] mazeMatrix, int h, int w)
        {
            this.mazeMatrix = mazeMatrix;
            this.height = h;
            this.width = w;
        }

        public ReshapeAbleMatrixMaze(char[,] mazeMatrix)
        {
            this.mazeMatrix = mazeMatrix;
        }

        public char[,] GetMazeMatrix()
        {
            return this.mazeMatrix;
        }

        public void closeAllDoors()
        {
            for (int i = 0; i < this.height * 2; i++)
            {
                for (int j = 0; j < this.width * 2; j++)
                {
                    if (i % 2 == 1 || j % 2 == 1)
                        this.mazeMatrix[i, j] = WALL;
                    else
                        this.mazeMatrix[i, j] = PASS;
                }
            }
        }

        public override List<IMazePosition> getAvailablePositionsFrom(IMazePosition position)
        {
            List<IMazePosition> result = new List<IMazePosition>();
            MatrixPosition pos = position as MatrixPosition;
            if (pos.Colomn % 2 == 1 || pos.Row % 2 == 1)
            {
                return result;
            }
            // upper cell
            if (pos.Row > 0)
            {
                result.Add(new MatrixPosition(pos.Row - 2, pos.Colomn));
            }
            // lower cell 
            if (pos.Row < this.height - 1)
            {
                result.Add(new MatrixPosition(pos.Row + 2, pos.Colomn));
            }
            // right cell
            if (pos.Colomn < this.width - 1)
            {

                result.Add(new MatrixPosition(pos.Row, pos.Colomn + 2));
            }
            // left cell 
            if (pos.Colomn > 0)
            {
                result.Add(new MatrixPosition(pos.Row, pos.Colomn - 2));
            }
            return result;
        }
    }
}