using MazeServer.Utilities;
using System;

namespace MazeServer.Model.JsonOptions
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
            int rows, cols;
            string solDisplayStr = "";
            if (!int.TryParse(AppSettings.Settings["rows"], out rows) ||
                !int.TryParse(AppSettings.Settings["cols"], out cols) ||
                this.sol.Length != (cols * 2 - 1) * (rows * 2 - 1))
            {
                solDisplayStr = this.sol + "\n";
            }
            else
            {
                for (int i = 0; i < rows * 2 - 1; i++)
                {
                    if (i > 0) { solDisplayStr += "       "; }
                    solDisplayStr += sol.Substring(i * (cols * 2 - 1),
                                                    rows * 2 - 1) + "\n";
                }
            }
            return string.Format("Solution for Maze: {0}\n"
                                + "solution is: {1}"
                                + "start: {2}, end: {3}",
                                this.name, solDisplayStr, this.start, this.end);
        }
    }
}
