using System;
using System.Linq;

namespace MazeServer.Model.Options
{
    /// <summary>
    /// Option that closes a multiplayer game.
    /// </summary>
    /// <seealso cref="MazeServer.Model.Options.Commandable" />
    class CloseOption : Commandable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseOption"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseOption(IModel model)
        {
           this.model = model;
        }

        /// <summary>
        /// Checks if a given client is in a game. If so, he is removed, and if the game contains 0 players
        /// it is removed from the model.
        /// Otherwise, nothing is done.
        /// </summary>
        /// <param name="from">the client that sent the command.</param>
        /// <param name="commandParsed">The parsed command.</param>
        public override void Execute(object from, string[] commandParsed)
        {
            MultiplayerGame game = model.IsClientInGame(from);
            if (game == null) return;


            game.RemoveClient(from);
            // game is empty
            if (game.Count == 0) model.RemoveMultiplayerGame(game.GetName());
        }

        /// <summary>
        /// Checks if the command contains one word - "close".
        /// </summary>
        /// <param name="commandParsed">The parsed command.</param>
        /// <returns>'true' if command is valid.
        ///          'false' if command is invalid.</returns>
        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;

            return true;
        }
    }
}
