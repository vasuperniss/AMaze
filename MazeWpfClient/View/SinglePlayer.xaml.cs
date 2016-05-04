using MazeWpfClient.Model;
using MazeWpfClient.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MazeWpfClient.View
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {
        /// <summary>
        /// the View Model
        /// </summary>
        private SinglePlayerViewModel vm;
        /// <summary>
        /// The main menu window
        /// </summary>
        private Window mainWindow;
        /// <summary>
        /// The music player
        /// </summary>
        private MusicPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayer"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="main">The main.</param>
        public SinglePlayer(ISinglePlayerModel model, Window main)
        {
            InitializeComponent();
            // play the single player music
            this.player = new MusicPlayer("singleplayer.mp3");
            this.player.Play();
            this.mainWindow = main;
            // create a ViewModel for the singleplayer with the given model
            this.vm = new SinglePlayerViewModel(model);
            this.DataContext = this.vm;
            this.mazeCtrl.DataContext = this.vm;
        }

        /// <summary>
        /// Handles the onKeyUp event of the window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void window_onKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.Focus();
            // check if the pressed key is a movement key
            switch (e.Key) {
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
        /// Servers the disconnected changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void ServerDisconnectedChanged(object sender, TextChangedEventArgs e)
        {
            string message = (sender as TextBox).Text;
            if(message.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show(message,
                                        "No connection", MessageBoxButton.OK);
                // close the single player window
                this.Close();
                this.mainWindow.Show();
            }
        }

        /// <summary>
        /// Creates the clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void CreateClicked(object sender, RoutedEventArgs e)
        {
            if (this.mazeNameTxt.Text.Length > 0)
            {
                this.vm.CreateNewMaze(this.mazeNameTxt.Text);
                this.mazeCtrl.Focus();
        }
        }

        /// <summary>
        /// Shows the hint clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void ShowHintClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowHint();
        }

        /// <summary>
        /// Restarts the clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void RestartClicked(object sender, RoutedEventArgs e)
        {
            this.vm.RestartMaze();
        }

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

        private void OnClosed(object sender, System.EventArgs e)
        {
            this.player.Stop();

            MainWindow main = this.mainWindow as MainWindow;
            if(main != null)
                main.GetPlayer().Play();
            // go back to the main menu
            this.mainWindow.Show();
        }
    }
}
