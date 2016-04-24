using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using System.ComponentModel;
using System;

namespace MazeWpfClient.Model
{
    public class MazeModel : IMazeModel
    {
        private IServer server;
        private JsonAnswerFactory answersFactory;

        private SinglePlayerMaze singlePlayerMaze;
        private MultiPlayerMaze multiPlayerMaze;
        private bool isSinglePlayerGame;
        private int width;
        private int height;

        public event PropertyChangedEventHandler PropertyChanged;

        public MazeModel(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.server = null;
            this.singlePlayerMaze = null;
            this.multiPlayerMaze = null;

            this.answersFactory = new JsonAnswerFactory();
        }

        public bool Connect(string ip, int port)
        {
            if (this.server != null)
            {
                this.server.Close();
                this.server = null;
            }
            this.server = new MazeServer(ip, port);
            if (!this.server.Connect())
                return false;
            this.server.OnResponseReceived += this.ServerResponseHandler;
            return true;
        }

        public void LoadNewSinglePlayer(string mazeName)
        {
            this.server.SendRequest("generate " + mazeName + " 0");
            this.isSinglePlayerGame = true;
            this.MultiPlayerGame = null;
        }

        public void LoadNewMultiPlayer(string gameName)
        {
            this.server.SendRequest("multiplayer " + gameName);
            this.isSinglePlayerGame = false;
            this.SinglePlayerGame = null;
        }

        public void SolveMaze()
        {
            if (this.isSinglePlayerGame)
            {

            }
            else
            {

            }
        }

        public void GetHint()
        {
            if (this.isSinglePlayerGame)
            {

            }
            else
            {

            }
        }

        public SinglePlayerMaze SinglePlayerGame
        {
            get
            {
                return this.singlePlayerMaze;
            }
            set
            {
                this.singlePlayerMaze = value;
                this.NotifyPropertyChanged("SinglePlayerGame");
            }
        }

        public MultiPlayerMaze MultiPlayerGame
        {
            get
            {
                return this.multiPlayerMaze;
            }

            set
            {
                this.multiPlayerMaze = value;
                this.NotifyPropertyChanged("MultiPlayerGame");
            }
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void MakeMove(Move move)
        {
            if (this.isSinglePlayerGame && this.singlePlayerMaze != null)
            {
                this.singlePlayerMaze.Move(move);
            }
            else if (!this.isSinglePlayerGame && this.multiPlayerMaze != null)
            {
                
            }
        }

        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
            IServerAnswer answer = this.answersFactory.GetJsonAnswer(args.Response);
            switch ((answer as ServerAnswer).Type)
            {
                case 1:
                    this.SinglePlayerGame = new SinglePlayerMaze((answer as ServerAnswer).Content as GenerateAnswer, width, height);
                    break;
                case 2:
                    this.MultiPlayerGame = new MultiPlayerMaze((answer as ServerAnswer).Content as MultiplayerAnswer);
                    break;
            }
        }
    }
}
