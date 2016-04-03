using Maze_Library;
using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeServer.Model.Options
{
    class MultiplayerOption : Commandable
    {
        public MultiplayerOption(IModel model)
        {
            this.model = model;
        }

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
                // game contains 2 different clients after addition
                if (g.AddClient(from))
                {
                    //List<object> clients = g.GetClients();
                    string reply;
                    IMaze maze = g.GetMaze();
                    object client;

                    // generate answer for first client
                    GenerateAnswer firstClient = BuildMaze(maze, name + "_1");
                    // change starting position for second client
                    maze.ChangeStartPosition();
                    GenerateAnswer secondClient = BuildMaze(maze, name + "_2");

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

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;
            return true;
        }

        private string BuildReply(string name, int type, GenerateAnswer myMaze, GenerateAnswer otherMaze)
        {
            MultiplayerAnswer ans = new MultiplayerAnswer();

            ans.Name = name;
            ans.MazeName = name + " maze";
            ans.You = myMaze;
            ans.Other = otherMaze;

            return new Answer().GetJSONAnswer(type, ans);
        }

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
