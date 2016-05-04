using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    /// <summary>
    /// PropertyChangedEventArgs with type of player.
    /// </summary>
    /// <seealso cref="System.ComponentModel.PropertyChangedEventArgs" />
    class MultiPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// The type of the player that sent the notification.
        /// </summary>
        private PlayerType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        public MultiPropertyChangedEventArgs(PlayerType type, string propertyName) : base(propertyName)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the player type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public PlayerType Type
        {
            get { return this.type; }
        }
    }
}
