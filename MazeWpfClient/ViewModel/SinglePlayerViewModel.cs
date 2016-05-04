using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    /// <summary>
    /// Single player ViewModel (VM)
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class SinglePlayerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The single player model
        /// </summary>
        private ISinglePlayerModel model;
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Model control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        /// <summary>
        /// Move the player to the m direction.
        /// </summary>
        /// <param name="m">The Movement to do.</param>
        public void Move(Move m)
        {
            if (!this.model.WonGame)
                this.model.MakeMove(m);
        }

        /// <summary>
        /// Gets or sets the name of the v m_ maze.
        /// </summary>
        /// <value>
        /// The name of the v m_ maze.
        /// </value>
        public string VM_MazeName
        {
            get { return this.model.MazeName; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the v m_ maze string.
        /// </summary>
        /// <value>
        /// The v m_ maze string.
        /// </value>
        public string VM_MazeString
        {
            get { return this.model.MazeString; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the v m_ player position.
        /// </summary>
        /// <value>
        /// The v m_ player position.
        /// </value>
        public string VM_PlayerPosition
        {
            get {
                    Model.Answer.MazePosition pos = this.model.PlayerPosition;
                    if (pos != null)
                        return pos.ToString();
                    else
                        return "";
                }
            set {; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [v m_ won game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [v m_ won game]; otherwise, <c>false</c>.
        /// </value>
        public bool VM_WonGame
        {
            get { return this.model.WonGame; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the v m_ solution string.
        /// </summary>
        /// <value>
        /// The v m_ solution string.
        /// </value>
        public string VM_SolutionString
        {
            get { return this.model.SolutionString; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the v m_ hint.
        /// </summary>
        /// <value>
        /// The v m_ hint.
        /// </value>
        public string VM_Hint
        {
            get {
                Model.Answer.MazePosition pos = this.model.Hint;
                if (pos != null)
                    return pos.ToString();
                else
                    return "";
                }
            set {; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [v m_ lost game].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [v m_ lost game]; otherwise, <c>false</c>.
        /// </value>
        public bool VM_LostGame
        {
            get {return this.model.LostGame; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the v m_ server disconnected.
        /// </summary>
        /// <value>
        /// The v m_ server disconnected.
        /// </value>
        public string VM_ServerDisconnected
        {
            get
            {
                return this.model.isConnected ? "" :
                    "Lost connection to server. Close to go back to main menu.";
            }
            set {; }
        }

        /// <summary>
        /// Creates the new maze.
        /// </summary>
        /// <param name="text">The name of the maze.</param>
        internal void CreateNewMaze(string text)
        {
            this.model.LoadNewGame(text);
        }

        /// <summary>
        /// Restarts the maze.
        /// </summary>
        internal void RestartMaze()
        {
            this.model.Restart();
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
