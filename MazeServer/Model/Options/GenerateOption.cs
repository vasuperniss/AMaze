﻿using System.Linq;
using Maze_Library;
using Maze_Library.Maze.WallBreakers;
using MazeServer.Utilities;
using Maze_Library.Maze;
using Maze_Library.Maze.Matrix;

namespace MazeServer.Model.Options
{
    class GenerateOption : Commandable
    {
        public GenerateOption(IModel model)
        {
            this.model = model;
            fetcher = new AppConfigSettingsFetcher();
        }

        public override string Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            string type = commandParsed[2];
            IMaze maze = CreateMaze(int.Parse(type));
            model.AddMaze(name, maze);

            string reply = "{\"Type\":1,\"Content\":{";
            reply += JsonConverter.NameToJson(name) + ",";
            reply += JsonConverter.MazeToJson(maze) + ",";
            reply += JsonConverter.PointToJson("Start",maze.GetStartPosition()) + ",";
            reply += JsonConverter.PointToJson("End", maze.GetFinishPosition()) + "}}";

            return reply;
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 3) return false;
            string type = commandParsed[2];
            if (type != "0" && type != "1") return false;

            return true;
        }

        public static IMaze CreateMaze(int type)
        {
            WallBreakerFactory breaker = new WallBreakerFactory((WallBreakerFactory.BreakingType)type);
            return new MatrixMaze(fetcher.GetHeight(), fetcher.GetWidth(), breaker);
        }
    }
}
