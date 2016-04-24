using MazeWpfClient.Model;
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
        private IMazeModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public SinglePlayerViewModel(IMazeModel model)
        {
            this.model = model;
            this.model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs("VM_" + e.PropertyName));
        }

        public SinglePlayerMaze VM_Maze
        {
            get { return this.model.SinglePlayerGame; }
        }
    }
}
