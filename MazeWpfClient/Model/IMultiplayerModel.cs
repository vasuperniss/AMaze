using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    interface IMultiPlayerModel : INotifyPropertyChanged
    {
        void LoadNewGame(string mazeName);
        void MakeMove(Move move);
        void SolveMaze();
        void GetHint();

        string MazeName { get; set; }
        string GameName { get; set; }
        string MazeString { get; set; }
        string SolutionString { get; set; }
        MazePosition PlayerPosition { get; set; }
        MazePosition OpponentPosition { get; set; }
        bool WonGame { get; set; }
        MazePosition Hint { get; set; }
    }
}
