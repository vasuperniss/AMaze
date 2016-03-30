using System;
using System.Linq;

namespace MazeServer.Model.Options
{
    class PlayOption : Commandable
    {
        public PlayOption(IModel model)
        {
            this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            return "play";
        }

        public override bool Validate(string[] commandParsed)
        {
            string[] directions = { "up","down","left","right" };
            if (commandParsed.Count() != 2) return false;

            return directions.Contains(commandParsed[1]);
        }
    }
}
