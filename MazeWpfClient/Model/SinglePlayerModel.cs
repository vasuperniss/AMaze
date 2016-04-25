using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using System.ComponentModel;
using System;

namespace MazeWpfClient.Model
{
    public class SinglePlayerModel : ISinglePlayerModel
    {
        private IServer server;
        private JsonAnswerFactory answersFactory;

        private SinglePlayerMaze singlePlayerMaze;

        private int cols;
        private int rows;

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
            this.server.SendRequest("generate " + mazeName + " 0");
        }

        public void SolveMaze()
        {
            this.server.SendRequest("solve " + this.singlePlayerMaze.Name + " 0");
        }

        public void GetHint()
        {

        }

        public string MazeName
        {
            get
            {
                return this.singlePlayerMaze != null ? this.singlePlayerMaze.Name : "Loading Maze...";
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

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void MakeMove(Move move)
        {
            if (this.singlePlayerMaze != null)
            {
                this.singlePlayerMaze.Move(move);
                this.NotifyPropertyChanged("PlayerPosition");
            }
        }

        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
            IServerAnswer answer = this.answersFactory.GetJsonAnswer(args.Response);
            switch ((answer as ServerAnswer).Type)
            {
                case 1:
                    this.singlePlayerMaze = new SinglePlayerMaze((answer as ServerAnswer).Content as GenerateAnswer, rows, cols);
                    this.MazeName = this.singlePlayerMaze.Name;
                    this.MazeString = this.singlePlayerMaze.Maze;
                    this.PlayerPosition = this.singlePlayerMaze.PlayerPosition;
                    break;
            }
        }
    }
}
