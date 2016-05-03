using MazeWpfClient.Model;
using MazeWpfClient.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
                    this.vm.Move(Move.Up);
                    break;
                case System.Windows.Input.Key.S:
                case System.Windows.Input.Key.Down:
                    this.vm.Move(Move.Down);
                    break;
                case System.Windows.Input.Key.D:
                case System.Windows.Input.Key.Right:
                    this.vm.Move(Move.Right);
                    break;
                case System.Windows.Input.Key.A:
                case System.Windows.Input.Key.Left:
                    this.vm.Move(Move.Left);
                    break;
            }
        }

        private void ServerDisconnectedChanged(object sender, TextChangedEventArgs e)
        {
            string message = (sender as TextBox).Text;
            if (message.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show(message, "No connection", MessageBoxButton.OK);
                this.Close();
                this.mainWindow.Show();
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
            if (this.gameNameText.Text.Length > 0)
            {
                this.model.SendMessage("close " + this.model.GameName);
            }
            this.mainWindow.Show();
        }
    }
}
