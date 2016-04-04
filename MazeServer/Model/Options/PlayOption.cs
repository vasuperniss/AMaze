using MazeServer.Model.JsonOptions;
using System.Linq;

namespace MazeServer.Model.Options
{
    /// <summary>
    /// Executes Play command.
    /// </summary>
    /// <seealso cref="MazeServer.Model.Options.Commandable" />
    class PlayOption : Commandable
    {
        /// <summary>
        /// The directions of movement.
        /// </summary>
        private string[] directions = { "up", "down", "left", "right" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayOption"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayOption(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Checks if the client is in a game.
        /// If so, the model attemps to fetch his opponent.
        ///     if the opponent exists, a reply is sent to him.
        ///     otherwise the function exists.
        /// if the game does not exist the function exists.
        /// </summary>
        /// <param name="from">the client that sent the command.</param>
        /// <param name="commandParsed">The parsed command.</param>
        public override void Execute(object from, string[] commandParsed)
        {
            string move = commandParsed[1];
            int commandType = 4;
            object otherClient;
            PlayAnswer ans = new PlayAnswer();

            // retrieve game
            MultiplayerGame game = model.IsClientInGame(from);
            if (game == null) return;

            // get the second player from the game
            game.RetrieveOtherClient(from, out otherClient);
            
            // a client tries to play while he's the only one in the game
            if (otherClient == null) return;

            ans.Name = game.GetName();
            ans.Move = move;
            string reply = new Answer().GetJSONAnswer(commandType, ans);
            
            model.CompletedTask(otherClient, new View.MessageEventArgs(reply));
        }

        /// <summary>
        /// Checks if the command has 2 words, and if the second word is a valid direction.
        /// </summary>
        /// <param name="commandParsed">The parsed command.</param>
        /// <returns>
        /// 'true' if command is valid.
        /// 'false' if command is invalid.
        /// </returns>
        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;

            return directions.Contains(commandParsed[1]);
        }
    }
}
