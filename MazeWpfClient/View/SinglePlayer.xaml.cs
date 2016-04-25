using MazeWpfClient.Model;
using MazeWpfClient.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MazeWpfClient.View
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayer : Window
    {
        private SinglePlayerViewModel vm;

        public SinglePlayer(ISinglePlayerModel model)
        {
            InitializeComponent();
            this.vm = new SinglePlayerViewModel(model);
            this.DataContext = this.vm;
            this.mazeCtrl.DataContext = this.vm;
        }

        private void window_onKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.W)
            {
                this.vm.Move(Move.Up);
            }
            else if (e.Key == System.Windows.Input.Key.S)
            {
                this.vm.Move(Move.Down);
            }
            else if (e.Key == System.Windows.Input.Key.D)
            {
                this.vm.Move(Move.Right);
            }
            else if (e.Key == System.Windows.Input.Key.A)
            {
                this.vm.Move(Move.Left);
            }
        }

        int counter = 0;

        private void Label_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as Label).Content = "changed : " + ++counter + " times";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            counter++;
        }
    }
}
