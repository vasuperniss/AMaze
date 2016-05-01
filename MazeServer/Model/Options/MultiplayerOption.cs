using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
using System.Linq;
using System.Text;

namespace MazeServer.Model.Options
{
    /// <summary>
    /// Initiates a multiplayer game.
    /// </summary>
    /// <seealso cref="MazeServer.Model.Options.Commandable" />
    class MultiplayerOption : Commandable
    {
        public MultiplayerOption(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Attempts to fetch a game from the model's data.
        /// If the game does not exist, a check is performed to see if the sending client is in a different game.
        ///     if so, the function closes.
        ///     Otherwise we create the game, and add the client to the game.
        /// Otherwise, if the game exists, the model attemps to add the client to the game.
        ///     if succeeded, the game now contains 2 different client, so a maze is generated,
        ///     given a name, and sent to the first client.
        ///     then a different starting position is given to the same maze and it is sent to the second client.
        /// if the client could not be added to the game the function exists.
        /// </summary>
        /// <param name="from">the client that sent the command.</param>
        /// <param name="commandParsed">The parsed command.</param>
        public override void Execute(object from, string[] commandParsed)
        {
            string name = commandParsed[1];
            int commandType = 3;
            MultiplayerGame g = model.GetMultiplayerGame(name);

            // game does not exist
            if(g == null)
            {
                // client is already in a different multiplayer game
                if (model.IsClientInGame(from) != null) return;

                // otherwise create a game
                IMaze maze = GenerateOption.CreateMaze(1);
                MultiplayerGame game = new MultiplayerGame(model, name, maze);
                game.AddClient(from);
                model.AddMultiplayerGame(name, game);
            }
            // game exists
            else
            {
                // add second(different) client to game, and if the game had not been started yet
                if (g.AddClient(from) && !g.IsInProgress())
                {
                    g.GameStarted();
                    string reply;
                    IMaze maze = g.GetMaze();
                    IMaze secondMaze;
                    object client;
                    string mazeName;

                    // generate answer for first client
                    mazeName = name + "_1";
                    this.model.AddMaze(mazeName, maze);
                    GenerateAnswer firstClient = BuildMaze(maze, mazeName);

                    // change starting position for second client
                    mazeName = name + "_2";
                    secondMaze = maze.CreateMazeChangeStartPosition();
                    this.model.AddMaze(mazeName, secondMaze);
                    GenerateAnswer secondClient = BuildMaze(secondMaze, mazeName);

                    // first client
                    g.RetrieveOtherClient(from, out client);
                    reply = BuildReply(name, commandType, firstClient, secondClient);
                    model.CompletedTask(client, new View.MessageEventArgs(reply));

                    // second client ('from' is the second player)
                    client = from;
                    reply = BuildReply(name, commandType, secondClient, firstClient);
                    model.CompletedTask(client, new View.MessageEventArgs(reply));
                }
            }
        }

        /// <summary>
        /// checks if the command contains 2 words.
        /// if the second word contains a space it is counted as a separate word,
        /// and thus will fail the check.
        /// </summary>
        /// <param name="commandParsed">The parsed command.</param>
        /// <returns>
        /// 'true' if command is valid.
        /// 'false' if command is invalid.
        /// </returns>
        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;
            return true;
        }

        /// <summary>
        /// Builds a reply.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="type">The type of command.</param>
        /// <param name="myMaze">the first client's maze.</param>
        /// <param name="otherMaze">The other client's maze.</param>
        /// <returns>reply string</returns>
        private string BuildReply(string name, int type, GenerateAnswer myMaze, GenerateAnswer otherMaze)
        {
            MultiplayerAnswer ans = new MultiplayerAnswer();

            ans.Name = name;
            ans.MazeName = name + " maze";
            ans.You = myMaze;
            ans.Other = otherMaze;

            return new Answer().GetJSONAnswer(type, ans);
        }

        /// <summary>
        /// Builds a maze.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="name">The name of it.</param>
        /// <returns>
        /// GenerateAnswer class that represents the server's answer
        /// to a generate request </returns>
        private GenerateAnswer BuildMaze(IMaze maze, string name)
        {
            GenerateAnswer ans = new GenerateAnswer();
            JsonOptions.MazePosition start = new JsonOptions.MazePosition();
            JsonOptions.MazePosition finish = new JsonOptions.MazePosition();

            ans.Name = name;
            StringBuilder sb = new StringBuilder(maze.ToString());
            sb.Replace("\n", "", 0, sb.Length);
            ans.Maze = sb.ToString();

            start.Row = maze.GetStartPosition().Row;
            start.Col = maze.GetStartPosition().Colomn;
            finish.Row = maze.GetFinishPosition().Row;
            finish.Col = maze.GetFinishPosition().Colomn;
            ans.Start = start;
            ans.End = finish;

            return ans;
        }
    }
}
