﻿using Maze_Library;
using System.Linq;

namespace MazeServer.Model.Options
{
    class MultiplayerOption : Commandable
    {
        public MultiplayerOption(IModel model)
        {
            this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            MultiplayerGame g = model.GetMultiplayerGame(name);

            // game does not exist
            if(g == null)
            {
                IMaze maze = GenerateOption.CreateMaze(1);
                MultiplayerGame game = new MultiplayerGame(model, name, maze);
                game.AddClient(from);
                model.AddMultiplayerGame(name, game);
                return null;
            }
            // game exists
            else
            {
                // client successully added
                // game contains 2 different clients
                if (g.AddClient(from))
                {

                }
            }

            return null;
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;
            return true;
        }
    }
}
