using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model.Options
{
    class CloseOption : Commandable
    {
        public CloseOption(IModel model)
        {
           this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            MultiplayerGame game = model.IsClientInGame(from);
            if (game == null) return null;

            game.RemoveClient(from);
            // game is empty
            if (game.Count == 0) model.RemoveMultiplayerGame(game.GetName());
            return null;
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 1) return false;

            return true;
        }
    }
}
