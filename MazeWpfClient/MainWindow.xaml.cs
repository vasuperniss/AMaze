using MazeWpfClient.Model;
using MazeWpfClient.Model.Server;
using MazeWpfClient.View;
using System;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MazeWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The Maze server comnunicator
        /// </summary>
        private IServer server;
        /// <summary>
        /// The settings to read from App.config
        /// </summary>
        static string[] settings = new string[] { "ip", "port",
                                              "rows", "cols" };
        /// <summary>
        /// The music player object for music playing
        /// </summary>
        private MusicPlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Reading App.config file
            if (!AppSettings.Settings.ReadAllSettings(settings))
            {
                // App.config is missing data or has errors in it
                this.Close();
            }
            // create the server and connect
            this.server = new MazeServer(AppSettings.Settings["ip"],
                                    int.Parse(AppSettings.Settings["port"]));
            this.server.Connect();
            // start the main menu music
            this.player = new MusicPlayer("menu.mp3");
            this.player.Play();
        }

        /// <summary>
        /// Handles the Clicked event of the SinglePlayerBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SinglePlayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));
            // create a singleplayer model and give it the server
            ISinglePlayerModel model = new SinglePlayerModel(server);
            // set up and launch the single player window
            SinglePlayer singlePlayer = new SinglePlayer(model, this);
            singlePlayer.Left = location.X;
            singlePlayer.Top = location.Y;

            this.player.Pause();
            singlePlayer.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Clicked event of the MultiplayerBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MultiplayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));
            // create the multiplayer model and give it the server
            IMultiPlayerModel model = new MultiPlayerModel(server);
            // set up and launch the multiplayer window
            MultiPlayer multiPlayer = new MultiPlayer(model, this);
            multiPlayer.Left = location.X;
            multiPlayer.Top = location.Y;

            this.player.Pause();
            multiPlayer.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Clicked event of the SettingsBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SettingsBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));
            // launch the settings window
            Settings settings = new Settings(this.server);
            settings.Left = location.X;
            settings.Top = location.Y;
            settings.ShowDialog();
            // reconnect to the new/old ip and port
            this.server.Close();
            this.server = new MazeServer(AppSettings.Settings["ip"],
                                    int.Parse(AppSettings.Settings["port"]));
            this.server.Connect();
        }

        /// <summary>
        /// Musics the toggle clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void MusicToggleClicked(object sender, RoutedEventArgs e)
        {
            if (this.player.Playing)
            {
                // stop playing the music
                this.player.Pause();
                this.musicImage.Source = new BitmapImage(
                        new Uri(@"/Resources/play.png", UriKind.Relative));
            }
            else
            {
                // continue playing the music
                this.player.Play();
                this.musicImage.Source = new BitmapImage(
                        new Uri(@"/Resources/pause.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// Called when [closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnClosed(object sender, System.EventArgs e)
        {
            // stop the music and disconnect from the server
            this.player.Stop();
            this.server.Close();
        }

        /// <summary>
        /// Gets the Music player.
        /// </summary>
        /// <returns>the music player</returns>
        public MusicPlayer GetPlayer()
        {
            return this.player;
        }
    }
}
