using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using System;

namespace MazeWpfClient.Model
{
    public class MultiPlayerMaze
    {
        private MultiplayerAnswer answer;
        private IServer server;
        private MazePosition playerPosition;
        private MazePosition opponenetPosition;
        private int rows;
        private int cols;
        private string solution = "";
        private MazePosition hint;

        public MultiPlayerMaze(MultiplayerAnswer answer, int rows, int cols, IServer server)
        {
            this.server = server;
            this.answer = answer;
            this.cols = cols;
            this.rows = rows;
            this.playerPosition = new MazePosition(this.answer.You.Start);
            this.opponenetPosition = new MazePosition(this.answer.Other.Start);
            this.hint = new MazePosition(this.answer.You.Start);
        }

        public string Name
        {
            get { return this.answer.Name; }
        }

        public string PlayerMaze
        {
            get { return this.answer.You.Maze; }
        }

        public string OpponentMaze
        {
            get { return this.answer.Other.Maze; }
        }

        public string PlayerMazeName
        {
            get { return this.answer.You.Name; }
        }

        public string OpponentMazeName
        {
            get { return this.answer.Other.Name; }
        }

        public MazePosition PlayerStart
        {
            get { return this.answer.You.Start; }
        }

        public MazePosition PlayerEnd
        {
            get { return this.answer.You.End; }
        }

        public MazePosition OpponentStart
        {
            get { return this.answer.Other.Start; }
        }

        public MazePosition OpponentEnd
        {
            get { return this.answer.Other.End; }
        }

        public MazePosition PlayerPosition
        {
            get { return this.playerPosition; }
            set { this.playerPosition = value; }
        }

        public MazePosition OpponentPosition
        {
            get { return this.opponenetPosition; }
            set { this.opponenetPosition = value; }
        }

        public string PlayerSolution
        {
            get
            {
                return this.solution;
            }
            set
            {
                this.solution = value;
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
            string direction = "play "+Enum.GetName(typeof(Move),move).ToLower();

            switch (move)
            {
                case Model.Move.Up:
                    if (this.playerPosition.Row > 0)
                    {
                        if (this.PlayerMaze[(this.playerPosition.Row - 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row -= 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
                case Model.Move.Down:
                    if (this.playerPosition.Row < this.rows * 2 - 2)
                    {
                        if (this.PlayerMaze[(this.playerPosition.Row + 1) * (this.cols * 2 - 1) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row += 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
                case Model.Move.Right:
                    if (this.playerPosition.Col < this.cols * 2 - 2)
                    {
                        if (this.PlayerMaze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col + 1] == '0')
                        {
                            this.PlayerPosition.Col += 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
                case Model.Move.Left:
                    if (this.playerPosition.Col > 0)
                    {
                        if (this.PlayerMaze[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col - 1] == '0')
                        {
                            this.PlayerPosition.Col -= 2;
                            moved = true;
                            this.server.SendRequest(direction);
                        }
                    }
                    break;
            }
            if (this.solution != "")
            {
                if (this.solution[this.playerPosition.Row * (this.cols * 2 - 1) + this.playerPosition.Col] == '2')
                {
                    this.hint = new MazePosition(this.playerPosition);
                }
            }
            return moved;
        }
    }
}