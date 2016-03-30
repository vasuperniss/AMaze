﻿using System.Collections.Generic;
using Maze_Library.Maze.WallBreakers;
using System.Text;
using System;

namespace Maze_Library.Maze.Matrix
{
    public class MatrixMaze : BaseMaze, IReshapeAbleMaze
    {
        private const char SOLPATH = '2';
        private const char WALL = '1';
        private const char PASS = '0';

        private char[,] mazeMatrix;
        private List<MazePosition> solution;
        private int width;
        private int height;

        public MatrixMaze(int height, int width, WallBreakerFactory BreakerFact)
        {
            this.mazeMatrix = new char[this.height = height * 2 - 1,
                                        this.width = width * 2 - 1];
            this.OpenAllDoors();
            BreakerFact.GetWallBreaker().BreakWalls(this);
            this.solution = null;

            Random r = new Random();
            int startCol = r.Next(this.width);
            startCol = startCol % 2 == 0 ? startCol : startCol - 1;
            int endCol = r.Next(this.width);
            endCol = endCol % 2 == 0 ? endCol : endCol - 1;
            this.startPosition = new MazePosition(0, startCol);
            this.endPosition = new MazePosition(this.height - 1, endCol);
        }

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

        public void ChangeDoorStateBetween(MazePosition first,
                                        MazePosition second, DoorState state)
        {
            int doorRow = first.Row + 1;
            if (second.Row < first.Row) { doorRow = first.Row - 1; }
            int doorCol = first.Colomn + 1;
            if (second.Colomn < first.Colomn) { doorCol = first.Colomn - 1; }
            if (state == DoorState.Opened)
            {
                this.mazeMatrix[doorRow, doorCol] = PASS;
            }
            else
            {
                this.mazeMatrix[doorRow, doorCol] = WALL;
            }
        }

        public override void SolveMaze(MazeSolverFactory solver)
        {
            this.solution = solver.SolveMaze(this);
        }
    }
}
