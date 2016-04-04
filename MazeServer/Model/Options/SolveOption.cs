using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
using System.Linq;
using System.Text;

namespace MazeServer.Model.Options
{
    /// <summary>
    /// Executes a solve command.
    /// </summary>
    /// <seealso cref="MazeServer.Model.Options.Commandable" />
    class SolveOption : Commandable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveOption"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveOption(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Checks if there exists a solution by the name the client requested.
        ///     if so, it is fetched and sent to him.
        /// Otherwise, the model attempts the fetch the maze to solve.
        ///     if the maze does not exist, the function exists.
        ///     if the maze exists a solution is generated and sent to the client.
        /// </summary>
        /// <param name="from">the client that sent the command.</param>
        /// <param name="commandParsed">The parsed command.</param>
        public override void Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            int type = int.Parse(commandParsed[2]);
            int commandType = 2;

            // check if there exists a solution
            string reply = model.GetMazeSolution(name);
            if (reply != null)
            {
                model.CompletedTask(from, new View.MessageEventArgs(reply));
                return;
            }

            // otherwise try to fetch the maze and generate a solution
            IMaze maze = model.GetMaze(name);
            if (maze == null) return;

            // maze exists but without a solution
            MazeSolverFactory solver = new MazeSolverFactory((WayToSolve)type);
            maze.SolveMaze(solver);
            string solution = maze.SolutionToString();

            // build reply
            reply = BuildReply(maze, name, solution, commandType);
            model.AddMazeSolution(name, reply);
            model.CompletedTask(from, new View.MessageEventArgs(reply));
        }

        /// <summary>
        /// Checks if the command has 3 words, and if the third words is either 0 or 1.
        /// </summary>
        /// <param name="commandParsed">The parsed command.</param>
        /// <returns>
        /// 'true' if command is valid.
        /// 'false' if command is invalid.
        /// </returns>
        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 3) return false;
            string type = commandParsed[2];
            if (type != "0" && type != "1") return false;

            return true;
        }

        /// <summary>
        /// Builds a reply for the client.
        /// Creates a SolveAnswer class which then is translated to JSON format.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="name">The name of the maze.</param>
        /// <param name="solution">The solution of the maze.</param>
        /// <param name="type">The type of command.</param>
        /// <returns>
        /// The reply in JSON format.
        /// </returns>
        private string BuildReply(IMaze maze, string name, string solution, int type)
        {
            SolveAnswer ans = new SolveAnswer();
            JsonOptions.MazePosition start = new JsonOptions.MazePosition();
            JsonOptions.MazePosition finish = new JsonOptions.MazePosition();

            start.Row = maze.GetStartPosition().Row;
            start.Col = maze.GetStartPosition().Colomn;
            finish.Row = maze.GetFinishPosition().Row;
            finish.Col = maze.GetFinishPosition().Colomn;

            StringBuilder sb = new StringBuilder(solution);
            sb.Replace("\n", "", 0, sb.Length);

            ans.Name = name;
            ans.Maze = sb.ToString();
            ans.Start = start;
            ans.End = finish;

            return new Answer().GetJSONAnswer(type, ans);
        }
    }
}
