﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Library;
using Maze_Library.Maze;

namespace MazeServer.Model.Options
{
    class GenerateMaze : Commandable
    {
        public GenerateMaze(IModel model)
        {
            Model = model;
        }

        public override string Execute()
        {
            IMaze maze = null; // GenerateMaze()
            string name = CommandParsed[1];
            Model.AddMaze(name, maze);

            return "generate";
        }

        public override bool Validate()
        {
            if (CommandParsed.Count() != 3) return false;
            string type = CommandParsed[2];

            if (type != "0" && type != "1") return false;

            return true;
        }
    }
}
