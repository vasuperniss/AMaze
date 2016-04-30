using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    public class SinglePlayerModel : ISinglePlayerModel
    {
        private IServer server;
        private JsonAnswerFactory answersFactory;
        private SinglePlayerMaze singlePlayerMaze;

        private int cols;
        private int rows;
        private bool showSolution;
        private bool showHint;

        public event PropertyChangedEventHandler PropertyChanged;

        public SinglePlayerModel(IServer server)
        {
            this.server = server;
            this.server.OnResponseReceived += this.ServerResponseHandler;
            this.cols = 24;
            this.rows = 8;
            this.answersFactory = new JsonAnswerFactory();
        }

        public void LoadNewGame(string mazeName)
        {
            this.server.SendRequest("generate " + mazeName + " 1");
            this.showSolution = false;
            this.showHint = false;
        }

        public void SolveMaze()
        {
            this.showSolution = true;
            this.NotifyPropertyChanged("SolutionString");
        }

        public void GetHint()
        {
            this.showHint = true;
            this.NotifyPropertyChanged("Hint");
        }

        public string MazeName
        {
            get
            {
                return this.singlePlayerMaze != null ? this.singlePlayerMaze.Name : "No Maze atm";
            }
            set
            {
                this.NotifyPropertyChanged("MazeName");
            }
        }

        public string MazeString
        {
            get
            {
                return this.singlePlayerMaze != null ? this.singlePlayerMaze.Maze : "";
            }
            set
            {
                this.NotifyPropertyChanged("MazeString");
            }
        }

        public MazePosition PlayerPosition
        {
            get
            {
                return this.singlePlayerMaze != null ? this.singlePlayerMaze.PlayerPosition : null;
            }
            set
            {
                this.NotifyPropertyChanged("PlayerPosition");
            }
        }

        public bool WonGame
        {
            get
            {
                if (this.singlePlayerMaze == null)
                {
                    return false;
                }
                return this.singlePlayerMaze.End.Row == this.PlayerPosition.Row &&
                    this.singlePlayerMaze.End.Col == this.PlayerPosition.Col;
            }
            set
            {
                this.NotifyPropertyChanged("WonGame");
            }
        }

        public string SolutionString
        {
            get
            {
                if (!this.showSolution) return "";
                return this.singlePlayerMaze.Solution;
            }
            set
            {
                this.NotifyPropertyChanged("SolutionString");
            }
        }

        public MazePosition Hint
        {
            get
            {
                if (!this.showHint) return null;
                return this.singlePlayerMaze.Hint;
            }
            set
            {
                this.NotifyPropertyChanged("Hint");
            }
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void MakeMove(Move move)
        {
            if (this.singlePlayerMaze != null)
            {
                if (this.singlePlayerMaze.Move(move))
                    this.showHint = false;
                this.NotifyPropertyChanged("PlayerPosition");
                this.NotifyPropertyChanged("WonGame");
                this.NotifyPropertyChanged("Hint");
            }
        }

        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
            IServerAnswer answer = this.answersFactory.GetJsonAnswer(args.Response);
            switch ((answer as ServerAnswer).Type)
            {
                case 1:
                    this.singlePlayerMaze = new SinglePlayerMaze((answer as ServerAnswer).Content as GenerateAnswer, rows, cols);
                    this.server.SendRequest("solve " + this.singlePlayerMaze.Name + " 0");
                    this.MazeName = this.singlePlayerMaze.Name;
                    this.MazeString = this.singlePlayerMaze.Maze;
                    this.PlayerPosition = this.singlePlayerMaze.PlayerPosition;
                    this.WonGame = false;
                    break;
                case 2:
                    if (this.singlePlayerMaze != null)
                    {
                        this.singlePlayerMaze.Solution = ((answer as ServerAnswer).Content as SolveAnswer).Maze;
                    }
                    else
                    {
                        this.singlePlayerMaze = new SinglePlayerMaze((answer as ServerAnswer).Content as SolveAnswer, rows, cols);
                        this.server.SendRequest("solve " + this.singlePlayerMaze.Name + " 0");
                        this.MazeName = this.singlePlayerMaze.Name;
                        this.MazeString = this.singlePlayerMaze.Maze;
                        this.PlayerPosition = this.singlePlayerMaze.PlayerPosition;
                        this.WonGame = false;
                    }
                    break;
            }
        }
    }
}
