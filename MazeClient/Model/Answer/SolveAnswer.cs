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
            int rows = 0, cols = 0;
            int.TryParse(AppSettings.Settings["rows"], out rows);
            int.TryParse(AppSettings.Settings["cols"], out cols);
            bool isCV = bool.Parse(AppSettings.Settings["isCoolVersion"]);
            string solDisplay = new MazeDrawer().getMazeToStr(this.sol,
                                                    13, isCV, rows, cols);

            return string.Format("Solution for Maze: {0}\n"
                                + "solution is: {1}\n"
                                + "start: {2}\nend: {3}",
                                this.name, solDisplay, this.start, this.end);
        }
    }
}
