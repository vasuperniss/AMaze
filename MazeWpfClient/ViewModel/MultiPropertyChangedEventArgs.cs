using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    class MultiPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        private PlayerType type;

        public MultiPropertyChangedEventArgs(PlayerType type, string propertyName) : base(propertyName)
        {
            this.type = type;
        }

        public PlayerType Type
        {
            get { return this.type; }
        }
    }
}
