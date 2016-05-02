using MazeWpfClient.Model;
using System;
using System.ComponentModel;

namespace MazeWpfClient.ViewModel
{
    public enum PlayerType { Player, Opponent }

    class MultiPlayerViewModel : INotifyPropertyChanged
    {
        private IMultiPlayerModel model;
        private MultiPlayerPlayerViewModel playerVM;
        private MultiPlayerOpponentViewModel opponentVM;
        public event PropertyChangedEventHandler PropertyChanged;

        public MultiPlayerViewModel(IMultiPlayerModel model)
        {
            this.model = model;
            this.playerVM = new MultiPlayerPlayerViewModel(this.model);
            this.opponentVM = new MultiPlayerOpponentViewModel(this.model);
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        public MultiPlayerPlayerViewModel Player
        {
            get { return this.playerVM; }
        }

        public MultiPlayerOpponentViewModel Opponent
        {
            get { return this.opponentVM; }
        }

        public string VM_MazeName
        {
            get { return this.model.GameName; }
            set {; }
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MultiPropertyChangedEventArgs m = e as MultiPropertyChangedEventArgs;
            if (m != null)
            {
                if (m.Type == PlayerType.Player)
                    this.playerVM.Notify(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
                    
                if (m.Type == PlayerType.Opponent)
                    this.opponentVM.Notify(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
            }
        }

        public void Move(Move m)
        {
            if (this.model.PlayerWonGame == false)
                this.model.MakeMove(m);
        }

        internal void CreateNewGame(string text)
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
