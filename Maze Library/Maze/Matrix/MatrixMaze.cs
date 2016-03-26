using System;
using Maze_Library.Maze;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }
}
