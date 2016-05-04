using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class MultiPlayerOpponentViewModel : INotifyPropertyChanged
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
        /// Initializes a new instance of the <see cref="MultiPlayerOpponentViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MultiPlayerOpponentViewModel(IMultiPlayerModel model)
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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string VM_MazeName
        {
            get { return this.model.OpponentMazeName; }
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
            get { return this.model.OpponentMazeString; }
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
                Model.Answer.MazePosition pos = this.model.OpponentPosition;
                if (pos != null)
                    return pos.ToString();
                else
                    return "";
            }
            set {; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether opponent won the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if won the game; otherwise, <c>false</c>.
        /// </value>
        public bool VM_WonGame
        {
            get { return this.model.OpponentWonGame; }
            set {; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether opponent lost the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if lost the game; otherwise, <c>false</c>.
        /// </value>
        public bool VM_LostGame
        {
            get { return this.model.PlayerWonGame; }
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
            get { return ""; }
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
            get { return ""; }
            set {; }
        }
    }
}
