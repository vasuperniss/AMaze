using MazeWpfClient.Model.Answer;

namespace MazeWpfClient.Model
{
    public class MultiPlayerMaze
    {
        private MultiplayerAnswer answer;

        public MultiPlayerMaze(MultiplayerAnswer multiplayerAnswer)
        {
            this.answer = multiplayerAnswer;
        }
    }
}