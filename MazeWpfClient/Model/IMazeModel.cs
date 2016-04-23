using System.ComponentModel;

namespace MazeWpfClient.Model
{
    public enum Move { Up, Down, Left, Right }

    interface IMazeModel : INotifyPropertyChanged
    {
        bool Connect(string ip, int port);
        void LoadNewSinglePlayer(string mazeName);
        void LoadNewMultiPlayer(string gameName);
        void MakeMove(Move move);
        void SolveMaze();
        void GetHint();

        SinglePlayerMaze SinglePlayerGame { get; set; }
        MultiPlayerMaze MultiPlayerGame { get; set; }
    }
}
