using System;

namespace MazeClient.Model.Answer
{
    class PlayAnswer : IServerAnswer
    {
        private string gameName;
        private string move;

        public string Name
        {
            get { return this.gameName; }
            set { this.gameName = value; }
        }

        public string Move
        {
            get { return this.move; }
            set { this.move = value; }
        }

        public override string ToString()
        {
            return string.Format("in that game: {0}, the other player"
               +" has made the [{1}] move", this.gameName, this.move);
        }
    }
}
