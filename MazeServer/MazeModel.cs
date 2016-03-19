﻿using Maze_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MazeServer
{
    class MazeModel
    {
        private Dictionary<string, IMaze> Mazes;
        //private Dictionary<string, Multiplayer> MpGames;

        public void GenerateMaze(string name, int type)
        {
            IMaze maze = null; // = Generate_Maze(type);
            Mazes.Add(name, maze);
        }

        public void CreateMultiplayerGame(string name)
        {
            // make a game
        }

        public IMaze GetMaze(string name)
        {
            IMaze maze;

            if (!Mazes.TryGetValue(name, out maze))
            {
                Console.WriteLine("MazeModel Error: No maze by name "+name);
                return null;
            }
            else
            {
                return maze;
            }
        }

        // public Multiplayer GetMpGame(string name) {}
    }
}
