﻿using Maze_Library;
using Maze_Library.Maze;
using MazeServer.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model
{
    class MasterModel : IModel
    {
        private Dictionary<string, IMaze> mazes;
        private Dictionary<string, string> mazeSolutions;
        private Dictionary<string, MultiplayerGame> mpGames;
        private string solutions_path;
        public event UpdateModel TaskCompleted;

        public MasterModel()
        {
            mazes = new Dictionary<string, IMaze>();
            mazeSolutions = new Dictionary<string, string>();
            mpGames = new Dictionary<string, MultiplayerGame>();

            string dir = Directory.GetCurrentDirectory();
            solutions_path = dir + "\\" + "mazeSolutions.json";

            if (File.Exists(solutions_path))
            {

            }
            else
            {
                File.Create(solutions_path);
            }
        }

        public void AddMaze(string name, IMaze maze)
        {
            if(!mazes.Keys.Contains(name)) mazes.Add(name, maze);
        }

        public void AddMultiplayerGame(string name, MultiplayerGame mp)
        {
            if(!mpGames.Keys.Contains(name)) mpGames.Add(name, mp);
        }

        public void AddMazeSolution(string name, string jsonDesc)
        {
            if (!mazeSolutions.Keys.Contains(name))
            {
                mazeSolutions.Add(name, jsonDesc);
                //File.WriteAllText(solutions_path, jsonDesc);
                File.AppendAllText(solutions_path, jsonDesc + '\n');
            }
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

        public void RemoveMultiplayerGame(string name)
        {
            mpGames.Remove(name);
        }

        public string GetMazeSolution(string name)
        {
            string sol;

            if (!mazeSolutions.TryGetValue(name, out sol))
            {
                Console.WriteLine("MasterModel Error: No maze solution by name " + name);
                return null;
            }
            else
            {
                return sol;
            }
        }

        public MultiplayerGame IsClientInGame(object client)
        {
            foreach (string key in mpGames.Keys)
            {
                if (mpGames[key].ContainsClient(client)) return mpGames[key];
            }

            return null;
        }

        public void CompletedTask(object from, MessageEventArgs reply)
        {
            TaskCompleted(from, reply);
        }

        private void CreateSolutionsFromFile()
        {

        }
    }
}
