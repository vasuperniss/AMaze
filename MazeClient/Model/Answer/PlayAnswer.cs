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

        public string GetStringRepresentation()
        {
            throw new NotImplementedException();
        }
    }
}
