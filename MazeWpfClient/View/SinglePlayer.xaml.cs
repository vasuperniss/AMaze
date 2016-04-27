﻿using MazeWpfClient.Model;
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

        public void CreateClicked(object sender, RoutedEventArgs e)
        {
            if (this.mazeNameTxt.Text.Length > 0)
                this.vm.CreateNewMaze(this.mazeNameTxt.Text);
        }

        public void ShowHintClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowHint();
        }

        public void ShowSolutionClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowSolution();
        }
    }
}