using MazeWpfClient.Model;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    class SinglePlayerViewModel : INotifyPropertyChanged
    {
        private ISinglePlayerModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public SinglePlayerViewModel(ISinglePlayerModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public void Move(Move m)
        {
            if (!this.model.WonGame)
                this.model.MakeMove(m);
        }

        public string VM_MazeName
        {
            get { return this.model.MazeName; }
            set {; }
        }

        public string VM_MazeString
        {
            get { return this.model.MazeString; }
            set {; }
        }

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

        public bool VM_WonGame
        {
            get { return this.model.WonGame; }
            set {; }
        }

        public string VM_SolutionString
        {
            get { return this.model.SolutionString; }
            set {; }
        }

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

        public string VM_ServerDisconnected
        {
            get
            {
                return this.model.isConnected ? "" : "Lost connection to server. Close to go back to main menu.";
            }
            set {; }
        }

        internal void CreateNewMaze(string text)
        {
            this.model.LoadNewGame(text);
        }

        internal void ShowSolution()
        {
            this.model.SolveMaze();
        }

        internal void ShowHint()
        {
            this.model.GetHint();
        }
    }
}
