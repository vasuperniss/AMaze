using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
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
            int commandType = 2;

            IMaze maze = model.GetMaze(name);
            if (maze == null) return null;

            // check if there exists a solution
            string reply = model.GetMazeSolution(name);
            if (reply != null) return reply;

            // otherwise generate one
            MazeSolverFactory solver = new MazeSolverFactory((WayToSolve)type);
            maze.SolveMaze(solver);
            string solution = maze.SolutionToString();

            // build reply
            reply = BuildReply(maze, name, solution, commandType);

            model.AddMazeSolution(name, reply);
            return reply;
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 3) return false;
            string type = commandParsed[2];
            if (type != "0" && type != "1") return false;

            return true;
        }

        private string BuildReply(IMaze maze, string name, string solution, int type)
        {
            SolveAnswer ans = new SolveAnswer();
            JsonOptions.MazePosition start = new JsonOptions.MazePosition();
            JsonOptions.MazePosition finish = new JsonOptions.MazePosition();

            start.Row = maze.GetStartPosition().Row;
            start.Col = maze.GetStartPosition().Colomn;
            finish.Row = maze.GetFinishPosition().Row;
            finish.Col = maze.GetFinishPosition().Colomn;

            ans.Name = name;
            ans.Maze = solution;
            ans.Start = start;
            ans.End = finish;

            return new Answer().GetJSONAnswer(type, ans);
        }
    }
}
