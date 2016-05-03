using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    class MultiPlayerPlayerViewModel : INotifyPropertyChanged
    {
        private IMultiPlayerModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public MultiPlayerPlayerViewModel(IMultiPlayerModel model)
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
            get { return this.model.PlayerMazeName; }
            set {; }
        }

        public string VM_MazeString
        {
            get { return this.model.PlayerMazeString; }
            set {; }
        }

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

        public bool VM_WonGame
        {
            get { return this.model.PlayerWonGame; }
            set {; }
        }

        public bool VM_LostGame
        {
            get { return this.model.OpponentWonGame; }
            set {; }
        }

        public string VM_SolutionString
        {
            get { return this.model.SolutionString; }
            set {; }
        }

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
