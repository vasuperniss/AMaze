using System.Linq;
using Maze_Library;
using Maze_Library.Maze.WallBreakers;
using MazeServer.Utilities;
using Maze_Library.Maze;
using Maze_Library.Maze.Matrix;
using MazeServer.Model.JsonOptions;
using System.Web.Script.Serialization;
using System.Text;

namespace MazeServer.Model.Options
{
    class GenerateOption : Commandable
    {
        public GenerateOption(IModel model)
        {
            this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            string type = commandParsed[2];
            int commandType = 1;

            IMaze maze = CreateMaze(int.Parse(type));
            model.AddMaze(name, maze);

            // build reply
            return BuildReply(maze, name, commandType);
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 3) return false;
            string type = commandParsed[2];
            if (type != "0" && type != "1") return false;

            return true;
        }

        private string BuildReply(IMaze maze, string name, int type)
        {
            GenerateAnswer ans = new GenerateAnswer();
            JsonOptions.MazePosition start = new JsonOptions.MazePosition();
            JsonOptions.MazePosition finish = new JsonOptions.MazePosition();

            ans.Name = name;
            ans.Maze = maze.ToString().Remove('\n');
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

        public static IMaze CreateMaze(int type)
        {
            WallBreakerFactory breaker = new WallBreakerFactory((WallBreakerFactory.BreakingType)type);
            MazeFactory factory = new MazeFactory(int.Parse(AppSettings.Settings["rows"]), int.Parse(AppSettings.Settings["cols"]));
            return factory.GetMaze(breaker);
        }
    }
}
