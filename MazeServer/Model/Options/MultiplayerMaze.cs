using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Interfaces;

namespace MazeServer.Model.Options
{
    class MultiplayerMaze : Commandable
    {
        public MultiplayerMaze(IMazeModel model)
        {
            Model = model;
        }

        public override string Execute()
        {
            //MultiplayerGame game = new MultiplayerGame();
            //string gameName = CommandParsed[1];
            //Model.AddMultiplayerGame(gameName, game);

            return "multiplayer";
        }

        public override bool Validate()
        {
            if (CommandParsed.Count() != 2) return false;
            return true;
        }
    }
}
