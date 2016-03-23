using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Options
{
    class SolveMaze : ICommandable
    {
        public void Execute()
        {
            throw new NotImplementedException();
            // do stuff

            // use communicator to send message to client.
        }

        public bool Validate(string command)
        {
            string[] words = command.Split(' ');
            string key = words[0];
            string type = words[2];

            if (words.Count() != 3) return false;
            if (type != "0" && type != "1") return false;

            return true;
        }
    }
}
