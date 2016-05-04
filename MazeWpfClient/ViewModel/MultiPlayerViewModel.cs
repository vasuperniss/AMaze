using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    /// <summary>
    /// Enum for distinguishing different notification types.
    /// </summary>
    public enum PlayerType { Player, Opponent, None }

    /// <summary>
    /// View model for multiplayer.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class MultiPlayerViewModel : INotifyPropertyChanged
    {
        private IMultiPlayerModel model;
        private MultiPlayerPlayerViewModel playerVM;
        private MultiPlayerOpponentViewModel opponentVM;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            this.playerVM = new MultiPlayerPlayerViewModel(this.model);
            this.opponentVM = new MultiPlayerOpponentViewModel(this.model);
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// Gets the player view model.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public MultiPlayerPlayerViewModel Player
        {
            get { return this.playerVM; }
        }

        /// <summary>
        /// Gets the opponent view model.
        /// </summary>
        /// <value>
        /// The opponent.
        /// </value>
        public MultiPlayerOpponentViewModel Opponent
        {
            get { return this.opponentVM; }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the v m_ maze.
        /// </value>
        public string VM_MazeName
        {
            get { return this.model.GameName; }
            set {; }
        }

        /// <summary>
        /// Handles notifications from model regarding changes of the multiplayer game.
        /// Forwards notifications from model regarding player/opponent changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MultiPropertyChangedEventArgs m = e as MultiPropertyChangedEventArgs;
            if (m != null)
            {
                switch (m.Type)
                {
                    // forward notification to player view model
                    case PlayerType.Player:
                        this.playerVM.Notify(this, new PropertyChangedEventArgs(e.PropertyName));
                        break;
                    // forward notification to player view model
                    case PlayerType.Opponent:
                        this.opponentVM.Notify(this, new PropertyChangedEventArgs(e.PropertyName));
                        break;
                    // handle notification about game
                    case PlayerType.None:
                        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
                        break;
                    default:
                        break;
                }                 
            }
        }

        /// <summary>
        /// Gets or sets the state of the server connection
        /// </summary>
        /// <value>
        /// The v m_ server disconnected.
        /// </value>
        public string VM_ServerDisconnected
        {
            get
            {
                return this.model.isConnected ? "" : "Lost connection to server. Close to go back to main menu.";
            }
            set {; }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="m">The m.</param>
        public void Move(Move m)
        {
            if (this.model.PlayerWonGame == false && this.model.OpponentWonGame == false)
                this.model.MakeMove(m);
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="text">The text.</param>
        internal void CreateNewGame(string text)
        {
            this.model.LoadNewGame(text);
        }

        /// <summary>
        /// Shows the solution.
        /// </summary>
        internal void ShowSolution()
        {
            this.model.SolveMaze();
        }

        /// <summary>
        /// Shows the hint.
        /// </summary>
        internal void ShowHint()
        {
            this.model.GetHint();
        }
    }
}
