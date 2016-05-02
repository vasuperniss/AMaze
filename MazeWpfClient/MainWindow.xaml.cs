using MazeWpfClient.Model;
using MazeWpfClient.Model.Server;
using MazeWpfClient.View;
using System.Windows;

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
        }

        private void SinglePlayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var location = this.PointToScreen(new Point(0, 0));
            ISinglePlayerModel model = new SinglePlayerModel(server);
            SinglePlayer singlePlayer = new SinglePlayer(model, this);
            singlePlayer.Left = location.X;
            singlePlayer.Top = location.Y;
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

        private void OnClosed(object sender, System.EventArgs e)
        {
            this.server.Close();
        }
    }
}
