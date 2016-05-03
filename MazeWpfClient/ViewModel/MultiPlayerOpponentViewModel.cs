using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    class MultiPlayerOpponentViewModel : INotifyPropertyChanged
    {
        private IMultiPlayerModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public MultiPlayerOpponentViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        public void Notify(object sender, PropertyChangedEventArgs e)
        {
            Model_PropertyChanged(sender, e);
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public string VM_MazeName
        {
            get { return this.model.OpponentMazeName; }
            set {; }
        }

        public string VM_MazeString
        {
            get { return this.model.OpponentMazeString; }
            set {; }
        }

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

        public bool VM_WonGame
        {
            get { return this.model.OpponentWonGame; }
            set {; }
        }

        public bool VM_LostGame
        {
            get { return this.model.PlayerWonGame; }
            set {; }
        }

        public string VM_SolutionString
        {
            get { return ""; }
            set {; }
        }

        public string VM_Hint
        {
            get { return ""; }
            set {; }
        }
    }
}
