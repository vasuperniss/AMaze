﻿using Maze_Library;
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
        private Dictionary<string, IMaze> mazes;
        //private Dictionary<string, MazeSolution> mazeSolutions;
        private Dictionary<string, MultiplayerGame> mpGames;
        public event UpdateModel TaskCompleted;

        public void AddMaze(string name, IMaze maze)
        {
            mazes.Add(name, maze);
        }

        public void AddMultiplayerGame(string name, MultiplayerGame mp)
        {
            mpGames.Add(name, mp);
        }

        public IMaze GetMaze(string name)
        {
            IMaze maze;

            if (!mazes.TryGetValue(name, out maze))
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

            if (!mpGames.TryGetValue(name, out game))
            {
                Console.WriteLine("MasterModel Error: No multiplayer game by name " + name);
                return null;
            }
            else
            {
                return game;
            }
        }

        public void CompletedTask(object from, MessageEventArgs reply)
        {
            TaskCompleted(from, reply);
        }
    }
}
