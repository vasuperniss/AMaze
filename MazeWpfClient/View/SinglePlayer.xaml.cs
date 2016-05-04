﻿using MazeWpfClient.Model;
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
        private SinglePlayerViewModel vm;
        private Window mainWindow;
        private MusicPlayer player;

        public SinglePlayer(ISinglePlayerModel model, Window main)
        {
            InitializeComponent();

            this.player = new MusicPlayer("singleplayer.mp3");
            this.player.Play();
            this.mainWindow = main;

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

        private void ServerDisconnectedChanged(object sender, TextChangedEventArgs e)
        {
            string message = (sender as TextBox).Text;
            if(message.Length > 0)
            {
                MessageBoxResult result = MessageBox.Show(message, "No connection", MessageBoxButton.OK);
                this.Close();
                this.mainWindow.Show();
            }
        }

        public void CreateClicked(object sender, RoutedEventArgs e)
        {
            if (this.mazeNameTxt.Text.Length > 0)
            {
                this.vm.CreateNewMaze(this.mazeNameTxt.Text);
                this.mazeCtrl.Focus();
                this.mazeNameTxt.Focusable = false;
            }
        }

        public void mazeNameTextMouseDown(object sender, RoutedEventArgs e)
        {
            this.mazeNameTxt.Focusable = true;
            this.mazeNameTxt.Focus();
        }

        public void ShowHintClicked(object sender, RoutedEventArgs e)
        {
            this.vm.ShowHint();
        }

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

            this.mainWindow.Show();
        }
    }
}
