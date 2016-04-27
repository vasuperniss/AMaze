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
        private ISinglePlayerModel model;
        private IServer server;

        public MainWindow()
        {
            InitializeComponent();
            this.server = new MazeServer("127.0.0.1", 55000);
            model = new SinglePlayerModel(server);
            if (server.Connect())
            {
                model.LoadNewGame("iMaze55");
            }
        }

        private void SinglePlayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            SinglePlayer singlePlayer = new SinglePlayer(this.model, this);
            singlePlayer.Show();
            this.Hide();
        }

        private void OnClosed(object sender, System.EventArgs e)
        {
            this.server.Close();
        }

        private void SettingsBtn_Clicked(object sender, RoutedEventArgs e)
        {
            double x, y;
            x = this.Width / 4;
            y = this.Height / 4;
            var location = this.PointToScreen(new Point(x, y));

            Settings settings = new Settings();
            settings.Left = location.X;
            settings.Top = location.Y;
            settings.ShowDialog();
        }
    }
}
