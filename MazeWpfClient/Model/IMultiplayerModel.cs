using MazeWpfClient.Model.Answer;
using System.ComponentModel;

namespace MazeWpfClient.Model
{
    public interface IMultiPlayerModel : INotifyPropertyChanged
    {
        void LoadNewGame(string mazeName);
        void MakeMove(Move move);
        void SolveMaze();
        void GetHint();
        void SendMessage(string message);

        string PlayerMazeName { get; set; }
        string OpponentMazeName { get; set; }
        string GameName { get; set; }
        string PlayerMazeString { get; set; }
        string OpponentMazeString { get; set; }
        string SolutionString { get; set; }
        MazePosition PlayerPosition { get; set; }
        MazePosition OpponentPosition { get; set; }
        bool PlayerWonGame { get; set; }
        bool OpponentWonGame { get; set; }
        MazePosition Hint { get; set; }
        bool isConnected { get; set; }
    }
}
