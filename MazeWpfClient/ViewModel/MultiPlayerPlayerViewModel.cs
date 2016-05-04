using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    /// <summary>
    /// View model for the player in the multiplayer.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class MultiPlayerPlayerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The model
        /// </summary>
        private IMultiPlayerModel model;
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerPlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        /// <summary>
        /// Calles the handle for notifications from model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        public void Notify(object sender, PropertyChangedEventArgs e)
        {
            Model_PropertyChanged(sender, e);
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
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string VM_MazeName
        {
            get { return this.model.PlayerMazeName; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>
        /// The maze string.
        /// </value>
        public string VM_MazeString
        {
            get { return this.model.PlayerMazeString; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public string VM_PlayerPosition
        {
            get
            {
                Model.Answer.MazePosition pos = this.model.PlayerPosition;
                if (pos != null)
                    return pos.ToString();
                else
                    return "";
            }
            set {; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player won the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if won the game; otherwise, <c>false</c>.
        /// </value>
        public bool VM_WonGame
        {
            get { return this.model.PlayerWonGame; }
            set {; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player lost the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if lost the game; otherwise, <c>false</c>.
        /// </value>
        public bool VM_LostGame
        {
            get { return this.model.OpponentWonGame; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the solution string.
        /// </summary>
        /// <value>
        /// The solution string.
        /// </value>
        public string VM_SolutionString
        {
            get { return this.model.SolutionString; }
            set {; }
        }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>
        /// The hint.
        /// </value>
        public string VM_Hint
        {
            get
            {
                Model.Answer.MazePosition pos = this.model.Hint;
                if (pos != null)
                    return pos.ToString();
                else
                    return "";
            }
            set {; }
        }
    }
}
