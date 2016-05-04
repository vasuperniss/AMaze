using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using System.ComponentModel;
using System;

namespace MazeWpfClient.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MazeWpfClient.Model.ISinglePlayerModel" />
    public class SinglePlayerModel : ISinglePlayerModel
    {
        /// <summary>
        /// The server
        /// </summary>
        private IServer server;
        /// <summary>
        /// The Server answers factory
        /// </summary>
        private JsonAnswerFactory answersFactory;
        /// <summary>
        /// The single player maze
        /// </summary>
        private SinglePlayerMaze singlePlayerMaze;

        /// <summary>
        /// The cols of the maze
        /// </summary>
        private int cols;
        /// <summary>
        /// The rows of the maze
        /// </summary>
        private int rows;
        /// <summary>
        /// The show solution boolean
        /// </summary>
        private bool showSolution;
        /// <summary>
        /// The show hint boolean
        /// </summary>
        private bool showHint;
        /// <summary>
        /// The is connected boolean
        /// </summary>
        private bool connected;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerModel"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public SinglePlayerModel(IServer server)
        {
            this.server = server;
            this.server.OnResponseReceived += this.ServerResponseHandler;
            // read the cols and rows from app.config
            this.cols = int.Parse(AppSettings.Settings["cols"]);
            this.rows = int.Parse(AppSettings.Settings["rows"]);
            this.answersFactory = new JsonAnswerFactory();
            this.connected = true;
        }

        /// <summary>
        /// Loads a new game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void LoadNewGame(string mazeName)
        {
            this.server.SendRequest("generate " + mazeName + " 1");
            this.showSolution = false;
            this.showHint = false;
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        public void SolveMaze()
        {
            this.showSolution = true;
            this.NotifyPropertyChanged("SolutionString");
        }

        /// <summary>
        /// Gets a hint.
        /// </summary>
        public void GetHint()
        {
            this.showHint = true;
            this.NotifyPropertyChanged("Hint");
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get
            {
                return this.singlePlayerMaze != null ?
                                this.singlePlayerMaze.Name : "Create a maze to play";
            }
            set
            {
                this.NotifyPropertyChanged("MazeName");
            }
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>
        /// The maze string.
        /// </value>
        public string MazeString
        {
            get
            {
                return this.singlePlayerMaze != null ?
                                            this.singlePlayerMaze.Maze : "";
            }
            set
            {
                this.NotifyPropertyChanged("MazeString");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool isConnected
        {
            get { return this.connected; }
            set
            {
                this.connected = value;
                this.NotifyPropertyChanged("ServerDisconnected");
            }
        }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public MazePosition PlayerPosition
        {
            get
            {
                return this.singlePlayerMaze != null ?
                                this.singlePlayerMaze.PlayerPosition : null;
            }
            set
            {
                this.NotifyPropertyChanged("PlayerPosition");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [lost game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [lost game]; otherwise, <c>false</c>.
        /// </value>
        public bool LostGame
        {
            get { return false; }
            set { this.NotifyPropertyChanged("LostGame"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [won game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [won game]; otherwise, <c>false</c>.
        /// </value>
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
            set { this.NotifyPropertyChanged("WonGame"); }
        }

        /// <summary>
        /// Gets or sets the solution string.
        /// </summary>
        /// <value>
        /// The solution string.
        /// </value>
        public string SolutionString
        {
            get
            {
                if (!this.showSolution) return "";
                return this.singlePlayerMaze != null ? this.singlePlayerMaze.Solution : null;
            }
            set
            {
                this.NotifyPropertyChanged("SolutionString");
            }
        }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>
        /// The hint.
        /// </value>
        public MazePosition Hint
        {
            get
            {
                if (!this.showHint) return null;
                return this.singlePlayerMaze != null ? this.singlePlayerMaze.Hint : null;
            }
            set
            {
                this.NotifyPropertyChanged("Hint");
            }
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Makes the move on the Maze.
        /// </summary>
        /// <param name="move">The move.</param>
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

        /// <summary>
        /// Servers the response handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ResponseEventArgs"/> instance containing the event data.</param>
        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
            // server disconnected
            if(args.Response == null)
            {
                //this.NotifyPropertyChanged("ServerDisconnected");
                this.isConnected = false;
                return;
            }
            IServerAnswer answer = this.answersFactory.GetJsonAnswer(args.Response);
            switch ((answer as ServerAnswer).Type)
            {
                case 1:
                    // the server has responded with a new generated maze
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
                        // server has return the solution to the maze
                        this.singlePlayerMaze.Solution = ((answer as ServerAnswer).Content as SolveAnswer).Maze;
                    }
                    break;
            }      
        }

        /// <summary>
        /// Restarts the maze.
        /// </summary>
        public void Restart()
        {
            this.singlePlayerMaze.Restart();
            this.MazeName = this.singlePlayerMaze.Name;
            this.MazeString = this.singlePlayerMaze.Maze;
            this.PlayerPosition = this.singlePlayerMaze.PlayerPosition;
            this.WonGame = false;
        }
    }
}
