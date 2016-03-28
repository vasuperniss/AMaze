using System;

namespace MazeClient.Model.Answer
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

        public override string ToString()
        {
            return string.Format("Solution for Maze: {0}\n"
                                + "solution is: {1}\n"
                                + "start: {2}, end: {3}",
                                this.name, this.sol, this.start, this.end);
        }
    }
}
