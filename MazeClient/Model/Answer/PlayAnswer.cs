using System;

namespace MazeClient.Model
{
    class PlayAnswer : IServerAnswer
    {
        private string move;

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
