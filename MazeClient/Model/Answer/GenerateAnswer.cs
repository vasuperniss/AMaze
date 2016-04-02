using MazeClient.Model.Answer;
using System;
using System.Configuration;

namespace MazeClient.Model
{
    class GenerateAnswer : IServerAnswer
    {
        private string name;
        private string maze;
        private MazePosition start;
        private MazePosition end;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Maze
        {
            get { return this.maze; }
            set { this.maze = value; }
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
            string mazeDisplayStr = new MazeDrawer().getMazeToStr(this.maze,
                                                    6, isCV, rows, cols);

            return string.Format("Maze - name: {0}\nmaze: {1}\n"
                                + "start: {2}\nend: {3}",
                            this.name, mazeDisplayStr, this.start, this.end);
        }
    }
}
