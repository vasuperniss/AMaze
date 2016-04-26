using MazeWpfClient.Model;
using System.ComponentModel;
using System;

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
            get { return this.model.PlayerPosition.ToString(); }
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
            get { return this.model.Hint.ToString(); }
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
