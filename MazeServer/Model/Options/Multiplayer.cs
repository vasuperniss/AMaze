﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Options
{
    class Multiplayer : ICommandable
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool Validate(string command)
        {
            string[] words = command.Split(' ');
            string key = words[0];

            if (words.Count() != 2) return false;
            if (key != "multiplayer") return false;

            return true;
        }
    }
}
