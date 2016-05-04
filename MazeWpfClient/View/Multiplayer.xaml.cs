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
        /// <summary>
        /// </summary>
        private MultiPlayerViewModel vm;
        /// <summary>
        /// The main window
        /// </summary>
        private Window mainWindow;
        /// <summary>
        /// The model
        /// </summary>
        private IMultiPlayerModel model;
        /// <summary>
        /// The player
        /// </summary>
        private MusicPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayer"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="main">The main.</param>
        public MultiPlayer(IMultiPlayerModel model, Window main)
        {
            InitializeComponent();

            this.player = new MusicPlayer("multiplayer.mp3");
            this.player.Play();

            this.mainWindow = main;
            this.model = model;
            this.vm = new MultiPlayerViewModel(this.model);
            this.DataContext = this.vm;
            this.mazeCtrlPlayer.DataContext = this.vm.Player;
            this.mazeCtrlOpponent.DataContext = this.vm.Opponent;
        }

        /// <summary>
        /// Handles movement.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles server disconnection
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CreateClicked(object sender, RoutedEventArgs e)
        {
            if (this.gameNameText.Text.Length > 0)
            {
                this.vm.CreateNewGame(this.gameNameText.Text);
                this.gameNameText.Visibility = Visibility.Hidden;
                this.gameCreateButton.Visibility = Visibility.Hidden;
                this.gameNameLabel.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Show a hint
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ShowHintClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowHint();
        }

        /// <summary>
        /// Closes window and goes back to main window.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void NewGameClicked(object sender, RoutedEventArgs e)
        {
            this.OnClosed(sender, e);
            this.Close();
        }

        /// <summary>
        /// Toggles the music.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void MusicToggleClicked(object sender, RoutedEventArgs e)
        {
            if (this.player.Playing)
            {
                this.player.Pause();
                this.musicToggle.Content = "Play Music";
            }
            else
            {
                this.player.Play();
                this.musicToggle.Content = "Pause Music";
            }
        }

        /// <summary>
        /// Called when [closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnClosed(object sender, System.EventArgs e)
        {
            if (this.gameNameText.Text.Length > 0)
            {
                this.model.SendMessage("close " + this.model.GameName);
            }
            this.player.Stop();

            MainWindow main = mainWindow as MainWindow;
            if (main != null)
                main.MusicToggleClicked(null, null);
            
            this.mainWindow.Show();
        }
    }
}
