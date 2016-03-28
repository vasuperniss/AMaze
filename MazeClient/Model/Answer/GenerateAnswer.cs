using MazeClient.Model.Answer;
using System;

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
            return string.Format("Maze name : {0}\n"
                                + "maze : {1}\n"
                                + "start : {2}, end : {3}",
                                this.name, this.maze, this.start, this.end);
        }
    }
}
