﻿using MazeServer.Utilities;

namespace MazeServer.Model.JsonOptions
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
            int rows, cols;
            string mazeDisplayStr = "";
            if (!int.TryParse(AppSettings.Settings["rows"], out rows) ||
                !int.TryParse(AppSettings.Settings["cols"], out cols) ||
                this.maze.Length != (cols * 2 - 1) * (rows * 2 - 1))
            {
                mazeDisplayStr = this.maze + "\n";
            }
            else
            {
                for (int i = 0; i < rows * 2 - 1; i++)
                {
                    if (i > 0) { mazeDisplayStr += "       "; }
                    mazeDisplayStr += maze.Substring(i * (cols * 2 - 1),
                                                    rows * 2 - 1) + "\n";
                }
            }
            return string.Format("Maze name : {0}\nmaze : {1}"
                                + "start: {2}, end: {3}",
                            this.name, mazeDisplayStr, this.start, this.end);
        }
    }
}