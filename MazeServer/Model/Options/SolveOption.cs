using Maze_Library.Maze;
using MazeServer.Utilities;
using System.Linq;

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
            string commandType = "2";

            IMaze maze = model.GetMaze(name);
            if (maze == null) return null;

            // solve maze
            MazeSolverFactory solver = new MazeSolverFactory((WayToSolve)type);
            maze.SolveMaze(solver);
            string solution = maze.SolutionToString();
            string solutionJson = "{";

            // Build reply
            string reply = "{\"Type\":"+ commandType +",\"Content\":";

            solutionJson += JsonConverter.NameToJson(name) + ",";
            solutionJson += JsonConverter.MazeToJson(solution) + ",";

            solutionJson += JsonConverter.PointToJson("Start", maze.GetStartPosition()) + ",";
            solutionJson += JsonConverter.PointToJson("End", maze.GetFinishPosition()) + "}";

            model.AddMazeSolution(name, solution, solutionJson);
            return reply + solutionJson + "}";
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
