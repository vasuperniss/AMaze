using MazeWpfClient.Model.Answer;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace MazeWpfClient.Model
{
    public class SinglePlayerMaze
    {
        private GenerateAnswer answer;
        private MazePosition playerPosition;
        private int rows;
        private int cols;
        private string solution = "";
        private List<MazePosition> solutionPath;
        private MazePosition hint;
        private int hintIndex = 0;

        public SinglePlayerMaze(GenerateAnswer answer, int rows, int cols)
        {
            this.answer = answer;
            this.cols = cols;
            this.rows = rows;
            this.playerPosition = new MazePosition(this.answer.Start);
            this.hint = new MazePosition(this.answer.Start);
        }

        //public SinglePlayerMaze(SolveAnswer answer, int rows, int cols)
        //{
        //    this.answer = new GenerateAnswer(answer);
        //    this.cols = cols;
        //    this.rows = rows;
        //    this.playerPosition = new MazePosition(this.answer.Start);
        //    this.hint = new MazePosition(this.answer.Start);
        //}

        public string Name
        {
            get { return this.answer.Name; }
        }

        public string Maze
        {
            get { return this.answer.Maze; }
        }

        public MazePosition Start
        {
            get { return this.answer.Start; }
        }

        public MazePosition End
        {
            get { return this.answer.End; }
        }

        public MazePosition PlayerPosition
        {
            get { return this.playerPosition; }
            set
            {
                this.playerPosition = value;
            }
        }

        public string Solution
        {
            get
            {
                return this.solution;
            }
            set
            {
                if (this.solution == value) return;
                this.solution = value;
                this.solutionPath = new List<MazePosition>();
                this.solutionPath.Add(new MazePosition(this.Start));
                this.solutionPath.Add(new MazePosition(this.Start));
                while (this.solutionPath[this.solutionPath.Count - 1].Row != this.End.Row
                    || this.solutionPath[this.solutionPath.Count - 1].Col != this.End.Col)
                {
                    MazePosition currPos = this.solutionPath[this.solutionPath.Count - 1];
                    MazePosition prevPos = this.solutionPath[this.solutionPath.Count - 2];
                    if (currPos.Row > 0 && (this.solution[(currPos.Row - 1) * (this.cols * 2 - 1) + (currPos.Col)] == '2' || this.solution[(currPos.Row - 1) * (this.cols * 2 - 1) + (currPos.Col)] == '#') &&
                        (prevPos.Row != currPos.Row - 1 || prevPos.Col != currPos.Col))
                        this.solutionPath.Add(new MazePosition(currPos.Row - 1, currPos.Col));
                    else if (currPos.Row < this.rows * 2 - 2 && (this.solution[(currPos.Row + 1) * (this.cols * 2 - 1) + (currPos.Col)] == '2' || this.solution[(currPos.Row + 1) * (this.cols * 2 - 1) + (currPos.Col)] == '#') &&
                        (prevPos.Row != currPos.Row + 1 || prevPos.Col != currPos.Col))
                        this.solutionPath.Add(new MazePosition(currPos.Row + 1, currPos.Col));
                    else if (currPos.Col > 0 && (this.solution[(currPos.Row) * (this.cols * 2 - 1) + (currPos.Col - 1)] == '2' || this.solution[(currPos.Row) * (this.cols * 2 - 1) + (currPos.Col - 1)] == '#') &&
                        (prevPos.Row != currPos.Row || prevPos.Col != currPos.Col - 1))
                        this.solutionPath.Add(new MazePosition(currPos.Row, currPos.Col - 1));
                    else
                        this.solutionPath.Add(new MazePosition(currPos.Row, currPos.Col + 1));
                }
                this.solutionPath.RemoveAt(0);
                this.solutionPath.Add(this.End);
                this.solutionPath.Add(this.End);
                this.hint = this.solutionPath[2];
            }
        }

        public MazePosition Hint
        {
            get
            {
                return this.hint;
            }
            set
            {
                this.hint = value;
            }
        }

        public bool Move(Move move)
        {
            bool moved = false;
            switch (move)
            {
                case Model.Move.Up:
                    if (this.playerPosition.Row > 0)
                    {
                        if (this.Maze[(this.playerPosition.Row - 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row -= 2;
                            moved = true;
                        }
                    }
                    break;
                case Model.Move.Down:
                    if (this.playerPosition.Row < this.rows * 2 - 2)
                    {
                        if (this.Maze[(this.playerPosition.Row + 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row += 2;
                            moved = true;
                        }
                    }
                    break;
                case Model.Move.Right:
                    if (this.playerPosition.Col < this.cols * 2 - 2)
                    {
                        if (this.Maze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col + 1] == '0')
                        {
                            this.PlayerPosition.Col += 2;
                            moved = true;
                        }
                    }
                    break;
                case Model.Move.Left:
                    if (this.playerPosition.Col > 0)
                    {
                        if (this.Maze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col - 1] == '0')
                        {
                            this.PlayerPosition.Col -= 2;
                            moved = true;
                        }
                    }
                    break;
            }
            bool onSolution = false;
            for (int i = 0; i < this.solutionPath.Count; i++)
                if (this.playerPosition.Row == this.solutionPath[i].Row &&
                    this.playerPosition.Col == this.solutionPath[i].Col)
                {
                    this.Hint = this.solutionPath[i + 2];
                    onSolution = true;
                    this.hintIndex = i;
                    break;
                }
            if (!onSolution)
                this.Hint = this.solutionPath[this.hintIndex];
            return moved;
        }

        internal void Restart()
        {
            this.playerPosition = new MazePosition(this.Start);
            this.hintIndex = 0;
        }
    }
}