using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    public enum Move { Up, Down,Left,Right }

    public interface ISinglePlayerModel : INotifyPropertyChanged
    {
        void LoadNewGame(string mazeName);
        void MakeMove(Move move);
        void SolveMaze();
        void GetHint();

        string MazeName { get; set; }
        string MazeString { get; set; }
        string SolutionString { get; set; }
        MazePosition PlayerPosition { get; set; }
        bool WonGame { get; set; }
        MazePosition Hint { get; set; }
        bool isConnected { get; set; }
    }
}
