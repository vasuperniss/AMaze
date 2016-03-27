using System;

namespace MazeClient.Model.Answer
{
    class MultiplayerAnswer : IServerAnswer
    {
        private string gameName;
        private string mazeName;
        private string myMaze;
        private string othersMaze;

        public string Name
        {
            get { return this.gameName; }
            set { this.gameName = value; }
        }

        public string MazeName
        {
            get { return this.mazeName; }
            set { this.mazeName = value; }
        }

        public string You
        {
            get { return this.myMaze; }
            set { this.myMaze = value; }
        }

        public string Other
        {
            get { return this.othersMaze; }
            set { this.othersMaze = value; }
        }

        public string GetStringRepresentation()
        {
            throw new NotImplementedException();
        }
    }
}
