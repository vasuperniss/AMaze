using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    public enum Move { Up, Down, Left, Right }

    public interface ISinglePlayerModel : INotifyPropertyChanged
    {
        void LoadNewGame(string mazeName);
        void MakeMove(Move move);
        void SolveMaze();
        void GetHint();

        string MazeName { get; set; }
        string MazeString { get; set; }
        MazePosition PlayerPosition { get; set; }
    }
}
