using MazeWpfClient.Model;
using MazeWpfClient.Model.Answer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
