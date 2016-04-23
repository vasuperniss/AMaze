using MazeWpfClient.Model.Answer;

namespace MazeWpfClient.Model
{
    public class SinglePlayerMaze
    {
        private GenerateAnswer answer;
        private MazePosition playerPosition;
        private int height;
        private int width;

        public SinglePlayerMaze(GenerateAnswer answer, int width, int height)
        {
            this.answer = answer;
            this.width = width;
            this.height = height;
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
            set { this.playerPosition = value; }
        }

        public void Move(Move move)
        {
            switch (move)
            {
                case Model.Move.Up:
                    if (this.playerPosition.Col > 0)
                    {
                        if (this.Maze[(this.playerPosition.Row - 1) * (this.height) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row -= 2;
                        }
                    }
                    break;
                case Model.Move.Down:
                    if (this.playerPosition.Col < this.height)
                    {
                        if (this.Maze[(this.playerPosition.Row + 1) * (this.height) + this.playerPosition.Col] == '0')
                        {
                            this.PlayerPosition.Row += 2;
                        }
                    }
                    break;
                case Model.Move.Right:
                    if (this.playerPosition.Row < this.width)
                    {
                        if (this.Maze[this.playerPosition.Row * (this.height) + this.playerPosition.Col + 1] == '0')
                        {
                            this.PlayerPosition.Col += 2;
                        }
                    }
                    break;
                case Model.Move.Left:
                    if (this.playerPosition.Row > 0)
                    {
                        if (this.Maze[this.playerPosition.Row * (this.height) + this.playerPosition.Col - 1] == '0')
                        {
                            this.PlayerPosition.Col -= 2;
                        }
                    }
                    break;
            }
        }
    }
}