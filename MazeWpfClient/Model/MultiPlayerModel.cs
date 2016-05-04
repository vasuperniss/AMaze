using System.ComponentModel;
using MazeWpfClient.Model.Answer;
using MazeWpfClient.Model.Server;
using MazeWpfClient.ViewModel;
using System;

namespace MazeWpfClient.Model
{
    /// <summary>
    /// Model for multiplayer.
    /// </summary>
    /// <seealso cref="MazeWpfClient.Model.IMultiPlayerModel" />
    class MultiPlayerModel : IMultiPlayerModel
    {
        /// <summary>
        /// The server
        /// </summary>
        private IServer server;
        /// <summary>
        /// The answers factory
        /// </summary>
        private JsonAnswerFactory answersFactory;
        /// <summary>
        /// The multiplayer maze
        /// </summary>
        private MultiPlayerMaze multiPlayerMaze;

        /// <summary>
        /// The columns
        /// </summary>
        private int cols;
        /// <summary>
        /// The rows
        /// </summary>
        private int rows;
        /// <summary>
        /// The show solution indicator
        /// </summary>
        private bool showSolution;
        /// <summary>
        /// The show hint indicator
        /// </summary>
        private bool showHint;
        /// <summary>
        /// The server connection indicator.
        /// </summary>
        private bool connected;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerModel"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        public MultiPlayerModel(IServer server)
        {
            this.server = server;
            this.server.OnResponseReceived += this.ServerResponseHandler;
            this.cols = int.Parse(AppSettings.Settings["cols"]);
            this.rows = int.Parse(AppSettings.Settings["rows"]);
            this.answersFactory = new JsonAnswerFactory();
            this.connected = true;
        }

        /// <summary>
        /// Sends the message to the server.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(string message)
        {
            this.server.SendRequest(message);
        }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string GameName
        {
            get
            {
                return (multiPlayerMaze != null) ? multiPlayerMaze.Name :  "";
            }
            set { this.NotifyPropertyChanged(PlayerType.None, "GameName"); }
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
                return this.multiPlayerMaze != null ? this.multiPlayerMaze.Hint : null;
            }
            set { this.NotifyPropertyChanged(PlayerType.Player, "Hint"); }
        }

        /// <summary>
        /// Gets or sets the name of the player maze.
        /// </summary>
        /// <value>
        /// The name of the player maze.
        /// </value>
        public string PlayerMazeName
        {
            get
            {
                return this.multiPlayerMaze != null ? this.multiPlayerMaze.PlayerMazeName : "Start a game.";
            }
            set { this.NotifyPropertyChanged(PlayerType.Player, "MazeName"); }
        }

        /// <summary>
        /// Gets or sets the name of the opponent maze.
        /// </summary>
        /// <value>
        /// The name of the opponent maze.
        /// </value>
        public string OpponentMazeName
        {
            get
            {
                return this.multiPlayerMaze != null ? this.multiPlayerMaze.OpponentMazeName : "Waiting for opponent..";
            }
            set { this.NotifyPropertyChanged(PlayerType.Opponent, "MazeName"); }
        }

        /// <summary>
        /// Gets or sets the player maze string.
        /// </summary>
        /// <value>
        /// The player maze string.
        /// </value>
        public string PlayerMazeString
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.PlayerMaze : ""; }
            set { this.NotifyPropertyChanged(PlayerType.Player, "MazeString"); }
        }

        /// <summary>
        /// Gets or sets the opponent maze string.
        /// </summary>
        /// <value>
        /// The opponent maze string.
        /// </value>
        public string OpponentMazeString
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.OpponentMaze : ""; }
            set { this.NotifyPropertyChanged(PlayerType.Opponent, "MazeString"); }
        }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public MazePosition PlayerPosition
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.PlayerPosition : null; }
            set { this.NotifyPropertyChanged(PlayerType.Player, "PlayerPosition"); }
        }

        /// <summary>
        /// Gets or sets the opponent position.
        /// </summary>
        /// <value>
        /// The opponent position.
        /// </value>
        public MazePosition OpponentPosition
        {
            get { return this.multiPlayerMaze != null ? this.multiPlayerMaze.OpponentPosition : null; }
            set { this.NotifyPropertyChanged(PlayerType.Opponent, "PlayerPosition"); }
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
                return this.multiPlayerMaze.PlayerSolution;
            }
            set { this.NotifyPropertyChanged(PlayerType.Player, "SolutionString"); }
        }

        /// <summary>
        /// Gets the hint.
        /// </summary>
        public void GetHint()
        {
            this.showHint = true;
            this.NotifyPropertyChanged(PlayerType.Player, "Hint");
        }

        /// <summary>
        /// Loads the new game.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
        public void LoadNewGame(string gameName)
        {
            this.server.SendRequest("multiplayer " + gameName);
            this.showSolution = false;
            this.showHint = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player won the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [player won game]; otherwise, <c>false</c>.
        /// </value>
        public bool PlayerWonGame
        {
            get
            {
                if (this.multiPlayerMaze == null)
                {
                    return false;
                }
                return this.multiPlayerMaze.PlayerEnd.Row == this.PlayerPosition.Row &&
                    this.multiPlayerMaze.PlayerEnd.Col == this.PlayerPosition.Col;
            }
            set
            {
                this.NotifyPropertyChanged(PlayerType.Player, "WonGame");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the opponent won the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [opponent won game]; otherwise, <c>false</c>.
        /// </value>
        public bool OpponentWonGame
        {
            get
            {
                if (this.multiPlayerMaze == null)
                {
                    return false;
                }
                return this.multiPlayerMaze.OpponentEnd.Row == this.OpponentPosition.Row &&
                    this.multiPlayerMaze.OpponentEnd.Col == this.OpponentPosition.Col;
            }
            set
            {
                this.NotifyPropertyChanged(PlayerType.Opponent, "WonGame");
            }
        }

        /// <summary>
        /// Makes the move.
        /// </summary>
        /// <param name="move">The move.</param>
        public void MakeMove(Move move)
        {
            if (this.multiPlayerMaze != null)
            {
                if (this.multiPlayerMaze.Move(move))
                    this.showHint = false;
                this.NotifyPropertyChanged(PlayerType.Player, "PlayerPosition");
                this.NotifyPropertyChanged(PlayerType.Player, "WonGame");
                this.NotifyPropertyChanged(PlayerType.Player, "LostGame");
                this.NotifyPropertyChanged(PlayerType.Player, "Hint");
            }
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        public void SolveMaze()
        {
            this.showSolution = true;
            this.NotifyPropertyChanged(PlayerType.Player, "SolutionString");
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
                this.NotifyPropertyChanged(PlayerType.None, "ServerDisconnected");
            }
        }

        /// <summary>
        /// Handles the server respones.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ResponseEventArgs"/> instance containing the event data.</param>
        private void ServerResponseHandler(object sender, ResponseEventArgs args)
        {
            // server disconnected
            if (args.Response == null)
            {
                this.isConnected = false;
                return;
            }
            IServerAnswer answer = this.answersFactory.GetJsonAnswer(args.Response);
            switch ((answer as ServerAnswer).Type)
            {
                //  received response with a solved maze
                case 2:
                    if (this.multiPlayerMaze != null)
                    {
                        this.multiPlayerMaze.PlayerSolution = ((answer as ServerAnswer).Content as SolveAnswer).Maze;
                    }
                    break;
                // received response with multiplayer data
                case 3:
                    this.multiPlayerMaze = new MultiPlayerMaze((answer as ServerAnswer).Content as MultiplayerAnswer, rows, cols, this.server);
                    // request solution
                    this.server.SendRequest("solve " + this.multiPlayerMaze.PlayerMazeName + " 0");

                    // set the data
                    this.PlayerMazeName = this.multiPlayerMaze.PlayerMazeName;
                    this.PlayerMazeString = this.multiPlayerMaze.PlayerMaze;
                    this.PlayerPosition = this.multiPlayerMaze.PlayerPosition;

                    this.OpponentMazeName = this.multiPlayerMaze.OpponentMazeName;
                    this.OpponentMazeString = this.multiPlayerMaze.OpponentMaze;
                    this.OpponentPosition = this.multiPlayerMaze.OpponentPosition;

                    this.PlayerWonGame = false;
                    this.OpponentWonGame = false;
                    break;
                // received response with move data
                case 4:
                    PlayAnswer playAns = (answer as ServerAnswer).Content as PlayAnswer;
                    if(playAns != null)
                    {
                        // capitalize first letter of move direction for enum
                        string playMove = char.ToUpper(playAns.Move[0]) + playAns.Move.Substring(1);
                        var content = (Move)Enum.Parse(typeof(Move), playMove);
                        // move opponent accordingly
                        switch (content)
                        {
                            case Move.Up:
                                this.multiPlayerMaze.OpponentPosition.Row -= 2;
                                break;
                            case Move.Down:
                                this.multiPlayerMaze.OpponentPosition.Row += 2;
                                break;
                            case Move.Left:
                                this.multiPlayerMaze.OpponentPosition.Col -= 2;
                                break;
                            case Move.Right:
                                this.multiPlayerMaze.OpponentPosition.Col += 2;
                                break;
                            default:
                                break;
                        }
                        // notify of movement
                        this.NotifyPropertyChanged(PlayerType.Opponent, "PlayerPosition");
                        this.NotifyPropertyChanged(PlayerType.Opponent, "WonGame");
                        this.NotifyPropertyChanged(PlayerType.Opponent, "LostGame");
                    }
                    break;
            }
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propName">Name of the property.</param>
        private void NotifyPropertyChanged(PlayerType type, string propName)
        {
            this.PropertyChanged?.Invoke(this, new MultiPropertyChangedEventArgs(type, propName));
        }
    }
}
