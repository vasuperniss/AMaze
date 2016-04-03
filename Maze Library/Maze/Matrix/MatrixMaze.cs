using System.Collections.Generic;
using Maze_Library.Maze.WallBreakers;
using System.Text;
using System;

/// <summary>
/// the Matrix namespace for Matrix based Mazes
/// </summary>
namespace Maze_Library.Maze.Matrix
{
    /// <summary>
    /// represents a Matrix based Maze
    /// </summary>
    /// <seealso cref="Maze_Library.Maze.BaseMaze" />
    /// <seealso cref="Maze_Library.Maze.IReshapeAbleMaze" />
    internal class MatrixMaze : BaseMaze, IReshapeAbleMaze
    {
        /// <summary>
        /// The solution path char
        /// </summary>
        private const char SOLPATH = '2';
        /// <summary>
        /// The wall char
        /// </summary>
        private const char WALL = '1';
        /// <summary>
        /// The pass char
        /// </summary>
        private const char PASS = '0';

        /// <summary>
        /// The maze matrix representation
        /// </summary>
        private char[,] mazeMatrix;
        /// <summary>
        /// The solution of the maze
        /// </summary>
        private List<MazePosition> solution;
        /// <summary>
        /// The width of the maze
        /// </summary>
        private int width;
        /// <summary>
        /// The height of the maze
        /// </summary>
        private int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixMaze"/> class.
        /// </summary>
        /// <param name="height">The height of the maze.</param>
        /// <param name="width">The width of the maze.</param>
        /// <param name="BreakerFact">The wall breaker factory.</param>
        public MatrixMaze(int height, int width, WallBreakerFactory BreakerFact)
        {
            this.mazeMatrix = new char[this.height = height * 2 - 1,
                                        this.width = width * 2 - 1];

            Random r = new Random();
            // randomly set a start and end positions
            int startCol = r.Next(this.width);
            startCol = startCol % 2 == 0 ? startCol : startCol - 1;
            int endCol = r.Next(this.width);
            endCol = endCol % 2 == 0 ? endCol : endCol - 1;
            this.startPosition = new MazePosition(0, startCol);
            this.endPosition = new MazePosition(this.height - 1, endCol);
            // initiallizes the matrix array of the maze will all doors open
            this.OpenAllDoors();
            // generates the maze
            BreakerFact.GetWallBreaker().BreakWalls(this);
            this.solution = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixMaze"/> class.
        /// using an existing maze
        /// </summary>
        /// <param name="maze">The maze to copy.</param>
        public MatrixMaze(MatrixMaze maze)
        {
            this.width = maze.width;
            this.height = maze.height;
            this.solution = maze.solution;
            this.startPosition = new MazePosition(maze.startPosition.Row,
                                                    maze.startPosition.Colomn);
            this.endPosition = new MazePosition(maze.endPosition.Row,
                                                    maze.endPosition.Colomn);
            this.mazeMatrix = new char[this.height, this.width];
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    this.mazeMatrix[i, j] = maze.mazeMatrix[i, j];
                }
            }
        }

        /// <summary>
        /// Gets the available positions from.
        /// </summary>
        /// <param name="pos">The position to move from.</param>
        /// <returns>the positions available</returns>
        public override List<MazePosition> GetAvailablePositionsFrom(MazePosition pos)
        {
            List<MazePosition> result = new List<MazePosition>();
            if (this.mazeMatrix[pos.Row, pos.Colomn] == WALL)
            {
                return result;
            }
            // upper cell
            if (pos.Row > 0)
            {
                if (this.mazeMatrix[pos.Row - 1, pos.Colomn] == PASS)
                {
                    result.Add(new MazePosition(pos.Row - 2, pos.Colomn));
                }
            }
            // lower cell 
            if (pos.Row < this.height - 1)
            {
                if (this.mazeMatrix[pos.Row + 1, pos.Colomn] == PASS)
                {
                    result.Add(new MazePosition(pos.Row + 2, pos.Colomn));
                }
            }
            // right cell
            if (pos.Colomn < this.width - 1)
            {
                if (this.mazeMatrix[pos.Row, pos.Colomn + 1] == PASS)
                {
                    result.Add(new MazePosition(pos.Row, pos.Colomn + 2));
                }
            }
            // left cell 
            if (pos.Colomn > 0)
            {
                if (this.mazeMatrix[pos.Row, pos.Colomn - 1] == PASS)
                {
                    result.Add(new MazePosition(pos.Row, pos.Colomn - 2));
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.height; i++)
            {
                if (i != 0)
                {
                    sb.Append("\n");
                }
                for (int j = 0; j < this.width; j++)
                {
                    sb.Append(this.mazeMatrix[i, j]);
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// Solutions to string.
        /// </summary>
        /// <returns>
        /// the maze string with the solution on it
        /// </returns>
        public override string SolutionToString()
        {
            StringBuilder sb = new StringBuilder(this.ToString());
            for (int i = 0; i < this.solution.Count - 1; i++)
            {
                sb.Replace(PASS, SOLPATH, this.solution[i].Row
                            * (1 + this.width) + this.solution[i].Colomn, 1);
                sb.Replace(PASS, SOLPATH, this.solution[i + 1].Row
                        * (1 + this.width) + this.solution[i + 1].Colomn, 1);
                MazePosition door = MazePosition.PositionBetween(this.solution[i],
                                                                this.solution[i + 1]);
                sb.Replace(PASS, SOLPATH, door.Row * (1 + this.width)
                                                                + door.Colomn, 1);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Opens all doors in the Maze.
        /// </summary>
        public void OpenAllDoors()
        {
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                        this.mazeMatrix[i, j] = WALL;
                    else
                        this.mazeMatrix[i, j] = PASS;
                }
            }
        }

        /// <summary>
        /// Closes all doors in the Maze.
        /// </summary>
        public void CloseAllDoors()
        {
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (i % 2 == 1 || j % 2 == 1)
                        this.mazeMatrix[i, j] = WALL;
                    else
                        this.mazeMatrix[i, j] = PASS;
                }
            }
        }

        /// <summary>
        /// Changes the door state between first and second.
        /// </summary>
        /// <param name="first">The first MazePosition.</param>
        /// <param name="second">The second MazePosition.</param>
        /// <param name="state">The door state to be set.</param>
        public void ChangeDoorStateBetween(MazePosition first,
                                        MazePosition second, DoorState state)
        {
            int doorRow = first.Row;
            if (second.Row < first.Row) { doorRow = first.Row - 1; }
            else if (second.Row > first.Row) { doorRow = first.Row + 1; }
            int doorCol = first.Colomn;
            if (second.Colomn < first.Colomn) { doorCol = first.Colomn - 1; }
            else if (second.Colomn > first.Colomn) { doorCol = first.Colomn + 1; }
            if (state == DoorState.Opened)
            {
                this.mazeMatrix[doorRow, doorCol] = PASS;
            }
            else
            {
                this.mazeMatrix[doorRow, doorCol] = WALL;
            }
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="solver">The solver.</param>
        public override void SolveMaze(MazeSolverFactory solver)
        {
            this.solution = solver.SolveMaze(this);
        }

        /// <summary>
        /// Changes the start position.
        /// </summary>
        public override void ChangeStartPosition()
        {
            Random r = new Random();
            int sCol;
            while ((sCol = r.Next(this.width)) == this.startPosition.Colomn)
            {
                sCol = sCol % 2 == 0 ? sCol : sCol - 1;
            }
            this.startPosition = new MazePosition(0, sCol);
        }

        /// <summary>
        /// Changes the end position.
        /// </summary>
        public override void ChangeEndPosition()
        {
            Random r = new Random();
            int eCol;
            while ((eCol = r.Next(this.width)) == this.endPosition.Colomn)
            {
                eCol = eCol % 2 == 0 ? eCol : eCol - 1;
            }
            this.endPosition = new MazePosition(this.height - 1, eCol);
        }

        /// <summary>
        /// Changes the end position.
        /// </summary>
        /// <param name="position">The new End position.</param>
        /// <returns>
        /// true if successfuly changed the position
        /// </returns>
        public bool ChangeEndPosition(MazePosition position)
        {
            if (position.Row == 0 || position.Row == this.height - 1
                || position.Colomn == 0 || position.Colomn == this.width - 1)
            {
                this.endPosition = position;
                return true;
            }
            return false;
        }
    }
}
