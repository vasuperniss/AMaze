using MazeWpfClient.Model;
using MazeWpfClient.ViewModel;
using System.Windows;

namespace MazeWpfClient.View
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class MultiPlayer : Window
    {
        private MultiPlayerViewModel vm;
        private Window mainWindow;
        private IMultiPlayerModel model;

        public MultiPlayer(IMultiPlayerModel model, Window main)
        {
            InitializeComponent();
            this.mainWindow = main;
            this.model = model;
            this.vm = new MultiPlayerViewModel(this.model);
            this.DataContext = this.vm;
            this.mazeCtrlPlayer.DataContext = this.vm.Player;
            this.mazeCtrlOpponent.DataContext = this.vm.Opponent;
        }

        private void window_onKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.W:
                case System.Windows.Input.Key.Up:
                    this.vm.Move(Move.up);
                    break;
                case System.Windows.Input.Key.S:
                case System.Windows.Input.Key.Down:
                    this.vm.Move(Move.down);
                    break;
                case System.Windows.Input.Key.D:
                case System.Windows.Input.Key.Right:
                    this.vm.Move(Move.right);
                    break;
                case System.Windows.Input.Key.A:
                case System.Windows.Input.Key.Left:
                    this.vm.Move(Move.left);
                    break;
            }
        }

        private void CreateClicked(object sender, RoutedEventArgs e)
        {
            if (this.gameNameText.Text.Length > 0)
                this.vm.CreateNewGame(this.gameNameText.Text);
        }

        private void ShowHintClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowHint();
        }

        public void ShowSolutionClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowSolution();
        }

        private void OnClosed(object sender, System.EventArgs e)
        {
            
            this.mainWindow.Show();
        }
    }
}
