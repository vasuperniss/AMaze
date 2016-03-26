using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Interfaces;

namespace MazeServer.Model.Options
{
    class PlayMaze : Commandable
    {
        public PlayMaze(IMazeModel model)
        {
            Model = model;
        }

        public override string Execute()
        {
            throw new NotImplementedException();

            return "play";
        }

        public override bool Validate()
        {
            string[] directions = { "up","down","left","right" };
            if (CommandParsed.Count() != 2) return false;

            return directions.Contains(CommandParsed[1]);
        }
    }
}
