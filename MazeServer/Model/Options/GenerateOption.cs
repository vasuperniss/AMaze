using System.Linq;
using Maze_Library.Maze.WallBreakers;
using MazeServer.Utilities;
using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
using System.Text;

namespace MazeServer.Model.Options
{
    /// <summary>
    /// Generates a maze.
    /// </summary>
    /// <seealso cref="MazeServer.Model.Options.Commandable" />
    class GenerateOption : Commandable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateOption"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateOption(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Checks if the requested maze exists. If so it is sent back to the client.
        /// Otherwise, it is created, added to the model's data,
        /// gets converted to JSON and is sent to the client.
        /// </summary>
        /// <param name="from">client that sent the command.</param>
        /// <param name="commandParsed">The parsed command.</param>
        public override void Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            string type = commandParsed[2];
            int commandType = 1;
            string reply;

            // maze exists
            IMaze existingMaze = model.GetMaze(name);
            if (existingMaze != null)
            {
                reply = BuildReply(existingMaze, name, commandType);
                model.CompletedTask(from, new View.MessageEventArgs(reply));
                return;
            }

            IMaze maze = CreateMaze(int.Parse(type));
            model.AddMaze(name, maze);

            // build reply
            reply = BuildReply(maze, name, commandType);
            model.CompletedTask(from, new View.MessageEventArgs(reply));
        }

        /// <summary>
        /// Checks if the command contains 3 words, and if the last word is either 1 or 0.
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
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="name">The name of the maze.</param>
        /// <param name="type">The type of the command.</param>
        /// <returns>reply string</returns>
        private string BuildReply(IMaze maze, string name, int type)
        {
            GenerateAnswer ans = new GenerateAnswer();
            JsonOptions.MazePosition start = new JsonOptions.MazePosition();
            JsonOptions.MazePosition finish = new JsonOptions.MazePosition();

            ans.Name = name;
            StringBuilder sb = new StringBuilder(maze.ToString());
            sb.Replace("\n", "", 0, sb.Length);
            ans.Maze = sb.ToString();

            start.Row = maze.GetStartPosition().Row;
            start.Col = maze.GetStartPosition().Colomn;
            finish.Row = maze.GetFinishPosition().Row;
            finish.Col = maze.GetFinishPosition().Colomn;
            ans.Start = start;
            ans.End = finish;

            return new Answer().GetJSONAnswer(type, ans);
        }

        /// <summary>
        /// Creates the maze.
        /// </summary>
        /// <param name="type">The type of algorithm to use to create the maze.</param>
        /// <returns>the created maze</returns>
        public static IMaze CreateMaze(int type)
        {
            WallBreakerFactory breaker = new WallBreakerFactory((WallBreakerFactory.BreakingType)type);
            MazeFactory factory = new MazeFactory(int.Parse(AppSettings.Settings["rows"]), int.Parse(AppSettings.Settings["cols"]));
            return factory.GetMaze(breaker);
        }
    }
}
