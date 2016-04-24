using MazeWpfClient.Model;
using MazeWpfClient.View;
using System.Windows;

namespace MazeWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMazeModel model;

        public MainWindow()
        {
            InitializeComponent();
            model = new MazeModel(10, 10);
            if (model.Connect("127.0.0.1", 55000))
                model.LoadNewSinglePlayer("iMaze55");
        }

        private void SinglePlayerBtn_Clicked(object sender, RoutedEventArgs e)
        {
            SinglePlayer singlePlayer = new SinglePlayer(this.model);
            singlePlayer.Show();
            this.Close();
        }
    }
}
