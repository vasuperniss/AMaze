using MazeWpfClient.Model;
using MazeWpfClient.ViewModel;
using System.Windows;

namespace MazeWpfClient.View
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {
        public SinglePlayer(IMazeModel model)
        {
            InitializeComponent();

            this.DataContext = new SinglePlayerViewModel(model);
        }
    }
}
