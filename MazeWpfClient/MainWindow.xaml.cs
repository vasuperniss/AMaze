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

        public MainWindow()
        {
            InitializeComponent();
            IServer server = new MazeServer("127.0.0.1", 55000);
            model = new SinglePlayerModel(server);
            if (server.Connect())
            {
                model.LoadNewGame("iMaze55");
            }
        }

        private void SinglePlayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            SinglePlayer singlePlayer = new SinglePlayer(this.model);
            singlePlayer.Show();
            this.Close();
        }
    }
}
