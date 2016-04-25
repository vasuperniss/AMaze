using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    public class SinglePlayerMaze
    {
        private GenerateAnswer answer;
        private MazePosition playerPosition;
        private int rows;
        private int cols;

        public SinglePlayerMaze(GenerateAnswer answer, int rows, int cols)
        {
            this.answer = answer;
            this.cols = cols;
            this.rows = rows;
            this.playerPosition = new MazePosition(this.answer.Start);
        }

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

        public void Move(Move move)
        {
            switch (move)
            {
                case Model.Move.Up:
                    if (this.playerPosition.Row > 0)
                    {
                        if (this.Maze[(this.playerPosition.Row - 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row -= 2;
                        }
                    }
                    break;
                case Model.Move.Down:
                    if (this.playerPosition.Row < this.rows * 2 - 2)
                    {
                        if (this.Maze[(this.playerPosition.Row + 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row += 2;
                        }
                    }
                    break;
                case Model.Move.Right:
                    if (this.playerPosition.Col < this.cols * 2 - 2)
                    {
                        if (this.Maze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col + 1] == '0')
                        {
                            this.PlayerPosition.Col += 2;
                        }
                    }
                    break;
                case Model.Move.Left:
                    if (this.playerPosition.Col > 0)
                    {
                        if (this.Maze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col - 1] == '0')
                        {
                            this.PlayerPosition.Col -= 2;
                        }
                    }
                    break;
            }
        }
    }
}