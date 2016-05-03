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
        private IServer server;
        static string[] settings = new string[] { "ip", "port",
                                              "rows", "cols" };
        private MusicPlayer player;

        public MainWindow()
        {
            InitializeComponent();


            // Reading App.config file
            if (!AppSettings.Settings.ReadAllSettings(settings))
            {
                this.Close();
            }
            this.server = new MazeServer(AppSettings.Settings["ip"],
                                    int.Parse(AppSettings.Settings["port"]));
            this.server.Connect();
            this.player = new MusicPlayer("menu.mp3");
            this.player.Play();
        }

        private void SinglePlayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));
            ISinglePlayerModel model = new SinglePlayerModel(server);
            SinglePlayer singlePlayer = new SinglePlayer(model, this);
            singlePlayer.Left = location.X;
            singlePlayer.Top = location.Y;

            this.player.Pause();
            singlePlayer.Show();
            this.Hide();
        }

        private void MultiplayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));
            IMultiPlayerModel model = new MultiPlayerModel(server);
            MultiPlayer multiPlayer = new MultiPlayer(model, this);
            multiPlayer.Left = location.X;
            multiPlayer.Top = location.Y;

            this.player.Pause();
            multiPlayer.Show();
            this.Hide();
        }

        private void SettingsBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));

            Settings settings = new Settings(this.server);
            settings.Left = location.X;
            settings.Top = location.Y;
            settings.ShowDialog();
            this.server.Close();
            this.server = new MazeServer(AppSettings.Settings["ip"],
                                    int.Parse(AppSettings.Settings["port"]));
            this.server.Connect();
        }

        private void MusicToggleClicked(object sender, RoutedEventArgs e)
        {
            if (this.player.Playing)
            {
                this.player.Pause();
                this.musicImage.Source = new BitmapImage(new Uri(@"/Resources/play.png", UriKind.Relative));
            }
            else
            {
                this.player.Play();
                this.musicImage.Source = new BitmapImage(new Uri(@"/Resources/pause.png", UriKind.Relative));
            }
        }

        private void OnClosed(object sender, System.EventArgs e)
        {
            this.player.Stop();
            this.server.Close();
        }

        public MusicPlayer GetPlayer()
        {
            return this.player;
        }
    }
}
