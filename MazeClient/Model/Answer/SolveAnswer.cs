using MazeClient.Model.Answer;
using System;

namespace MazeClient.Model
{
    class SolveAnswer : IServerAnswer
    {
        private string name;
        private string sol;
        private MazePosition start;
        private MazePosition end;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Maze
        {
            get { return this.sol; }
            set { this.sol = value; }
        }

        public MazePosition Start
        {
            get { return this.start; }
            set { this.start = value; }
        }

        public MazePosition End
        {
            get { return this.end; }
            set { this.end = value; }
        }

        public string GetStringRepresentation()
        {
            throw new NotImplementedException();
        }
    }
}
