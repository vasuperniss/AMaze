using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Interfaces;

namespace MazeServer.Model.Options
{
    class Close : Commandable
    {
        public override void Execute()
        {
            throw new NotImplementedException();
            // do stuff
        }

        public override bool Validate(string command)
        {
            string[] words = command.Split(' ');
            string key = words[0];

            if (words.Count() != 2) return false;

            return true;
        }
    }
}
