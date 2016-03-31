using MazeServer.Model.JsonOptions;
using System;
using System.Linq;

namespace MazeServer.Model.Options
{
    class PlayOption : Commandable
    {
        private string[] directions = { "up", "down", "left", "right" };

        public PlayOption(IModel model)
        {
            this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            string move = commandParsed[1];
            int commandType = 4;
            object otherClient;
            PlayAnswer ans = new PlayAnswer();

            // retrieve game
            MultiplayerGame game = model.IsClientInGame(from);
            if (game == null) return null;

            // get the second player from the game
            game.RetrieveOtherClient(from, out otherClient);
            
            // should never happen, but just in case
            if (otherClient == null) return null;

            ans.Name = game.GetName();
            ans.Move = move;
            string reply = new Answer().GetJSONAnswer(commandType, ans);
            
            model.CompletedTask(otherClient, new View.MessageEventArgs(reply));
            return null;
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;

            return directions.Contains(commandParsed[1]);
        }
    }
}
