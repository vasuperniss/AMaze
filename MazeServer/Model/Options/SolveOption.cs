using System.Linq;
using Maze_Library;

namespace MazeServer.Model.Options
{
    class SolveOption : Commandable
    {
        public SolveOption(IModel model)
        {
            this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            int type = int.Parse(commandParsed[2]);
            IMaze maze = model.GetMaze(name);

            if(maze != null)
            {
                // SolveMaze(maze, type);
                return "solved";
            }
            else
            {
                return null;
            }
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 3) return false;
            string type = commandParsed[2];
            if (type != "0" && type != "1") return false;

            return true;
        }
    }
}
