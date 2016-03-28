using Maze_Library;
using MazeServer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model
{
    class MasterModel : IModel
    {
        private Dictionary<string, IMaze> Mazes;
        private Dictionary<string, MultiplayerGame> MpGames;
        public event UpdateModel ModelChanged;

        public void AddMaze(string name, IMaze maze)
        {
            Mazes.Add(name, maze);
        }

        public void AddMultiplayerGame(string name, MultiplayerGame mp)
        {
            MpGames.Add(name, mp);
        }

        public IMaze GetMaze(string name)
        {
            IMaze maze;

            if (!Mazes.TryGetValue(name, out maze))
            {
                Console.WriteLine("MasterModel Error: No maze by name " + name);
                return null;
            }
            else
            {
                return maze;
            }
        }

        public MultiplayerGame GetMultiplayerGame(string name)
        {
            MultiplayerGame game;

            if (!MpGames.TryGetValue(name, out game))
            {
                Console.WriteLine("MasterModel Error: No multiplayer game by name " + name);
                return null;
            }
            else
            {
                return game;
            }
        }

        public void CompletedTask(string reply)
        {
            ModelChanged(this, new MessageEventArgs(reply));
        }
    }
}
