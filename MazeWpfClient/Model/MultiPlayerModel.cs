﻿using System.ComponentModel;
using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using MazeWpfClient.ViewModel;

namespace MazeWpfClient.Model
{
    class MultiPlayerModel : IMultiPlayerModel
    {
        private IServer server;
        private JsonAnswerFactory answersFactory;
        private MultiPlayerMaze multiPlayerMaze;

        private int cols;
        private int rows;
        private bool showSolution;
        private bool showHint;

        public event PropertyChangedEventHandler PropertyChanged;

        public MultiPlayerModel(IServer server)
        {
            this.server = server;
            this.server.OnResponseReceived += this.ServerResponseHandler;
            this.cols = 24;
            this.rows = 8;
            this.answersFactory = new JsonAnswerFactory();
        }

        public string GameName
        {
            get { return multiPlayerMaze.Name; }
            set { this.NotifyPropertyChanged(PlayerType.Player, "GameName"); }
        }

        public MazePosition Hint
        {
            get
            {
                if (!this.showHint) return null;
                return this.multiPlayerMaze.Hint;
            }
            set { this.NotifyPropertyChanged(PlayerType.Player, "Hint"); }
        }

        public string PlayerMazeName
        {
            get
            {
                return this.multiPlayerMaze != null ? this.multiPlayerMaze.PlayerMazeName : "Start a game.";
            }
            set { this.NotifyPropertyChanged(PlayerType.Player, "MazeName"); }
        }

        public string OpponentMazeName
        {
            get
            {
                return this.multiPlayerMaze != null ? this.multiPlayerMaze.OpponentMazeName : "Waiting for opponent..";
            }
            set { this.NotifyPropertyChanged(PlayerType.Opponent, "MazeName"); }
        }

        public string PlayerMazeString
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.PlayerMaze : ""; }
            set { this.NotifyPropertyChanged(PlayerType.Player, "MazeString"); }
        }

        public string OpponentMazeString
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.OpponentMaze : ""; }
            set { this.NotifyPropertyChanged(PlayerType.Opponent, "MazeString"); }
        }

        public MazePosition PlayerPosition
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.PlayerPosition : null; }
            set { this.NotifyPropertyChanged(PlayerType.Player, "PlayerPosition"); }
        }

        public MazePosition OpponentPosition
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.OpponentPosition : null; }
            set { this.NotifyPropertyChanged(PlayerType.Opponent, "PlayerPosition"); }
        }

        public string SolutionString
        {
            get
            {
                if (!this.showSolution) return "";
                return this.multiPlayerMaze.PlayerSolution;
            }
            set { this.NotifyPropertyChanged(PlayerType.Player, "SolutionString"); }
        }

        public void GetHint()
        {
            this.showHint = true;
            this.NotifyPropertyChanged(PlayerType.Player, "Hint");
        }

        public void LoadNewGame(string gameName)
        {
            this.server.SendRequest("multiplayer " + gameName);
            this.showSolution = false;
            this.showHint = false;
        }

        /* 
         * returns 0 if no one won.
         * return 1 if first player won.
         * return 2 if second player won.
         */
        public int WonGame
        {
            get
            {
                if (this.multiPlayerMaze == null)
                {
                    return 0;
                }
                if (this.multiPlayerMaze.PlayerEnd.Row == this.PlayerPosition.Row &&
                    this.multiPlayerMaze.PlayerEnd.Col == this.PlayerPosition.Col)
                    return 1;
                if (this.multiPlayerMaze.OpponentEnd.Row == this.OpponentPosition.Row &&
                    this.multiPlayerMaze.OpponentEnd.Col == this.OpponentPosition.Col)
                    return 2;

                return 0;
            }
            set
            {
                this.NotifyPropertyChanged(PlayerType.Player, "WonGame");
            }
        }

        public void MakeMove(Move move)
        {
            if (this.multiPlayerMaze != null)
            {
                if (this.multiPlayerMaze.Move(move))
                    this.showHint = false;
                this.NotifyPropertyChanged(PlayerType.Player, "PlayerPosition");
                this.NotifyPropertyChanged(PlayerType.Player, "WonGame");
                this.NotifyPropertyChanged(PlayerType.Player, "Hint");
            }
        }

        public void SolveMaze()
        {
            this.showSolution = true;
            this.NotifyPropertyChanged(PlayerType.Player, "SolutionString");
        }

        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
            IServerAnswer answer = this.answersFactory.GetJsonAnswer(args.Response);
            switch ((answer as ServerAnswer).Type)
            {
                case 1:
                    /*
                    this.singlePlayerMaze = new SinglePlayerMaze((answer as ServerAnswer).Content as GenerateAnswer, rows, cols);
                    this.server.SendRequest("solve " + this.singlePlayerMaze.Name + " 0");
                    this.MazeName = this.singlePlayerMaze.Name;
                    this.MazeString = this.singlePlayerMaze.Maze;
                    this.PlayerPosition = this.singlePlayerMaze.PlayerPosition;
                    this.WonGame = 0;
                    */
                    break;
                case 2:
                    if (this.multiPlayerMaze != null)
                    {
                        this.multiPlayerMaze.PlayerSolution = ((answer as ServerAnswer).Content as SolveAnswer).Maze;
                    }
                    else
                    {
                        /*
                        this.multiPlayerMaze = new MultiPlayerMaze((answer as ServerAnswer).Content as MultiplayerAnswer, rows, cols);
                        this.server.SendRequest("solve " + this.singlePlayerMaze.Name + " 0");
                        this.MazeName = this.singlePlayerMaze.Name;
                        this.MazeString = this.singlePlayerMaze.Maze;
                        this.PlayerPosition = this.singlePlayerMaze.PlayerPosition;
                        this.WonGame = 0;
                        */
                    }
                    break;
                case 3:
                    this.multiPlayerMaze = new MultiPlayerMaze((answer as ServerAnswer).Content as MultiplayerAnswer, rows, cols, this.server);
                    this.server.SendRequest("generate " + this.multiPlayerMaze.PlayerMazeName + " 0");
                    this.server.SendRequest("solve " + this.multiPlayerMaze.PlayerMazeName + " 0");

                    this.PlayerMazeName = this.multiPlayerMaze.PlayerMazeName;
                    this.PlayerMazeString = this.multiPlayerMaze.PlayerMaze;
                    this.PlayerPosition = this.multiPlayerMaze.PlayerPosition;

                    this.OpponentMazeName = this.multiPlayerMaze.OpponentMazeName;
                    this.OpponentMazeString = this.multiPlayerMaze.OpponentMaze;
                    this.OpponentPosition = this.multiPlayerMaze.OpponentPosition;

                    this.WonGame = 0;
                    break;
            }
        }

        private void NotifyPropertyChanged(PlayerType type, string propName)
        {
            this.PropertyChanged?.Invoke(this, new MultiPropertyChangedEventArgs(type, propName));
        }
    }
}
