using Maze_Library;
using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
using MazeServer.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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

            CreateDataFromFile();
        }

        public void AddMaze(string name, IMaze maze)
        {
            try
            {
                mazes.Add(name, maze);
                //File.AppendAllText(mazes_path, jsonDesc + '\n');
            }
            catch (ArgumentException)
            {
                // dictionary already contains the maze
            }
        }

        public void AddMultiplayerGame(string name, MultiplayerGame mp)
        {
            try
            {
                mpGames.Add(name, mp);
            }
            catch (ArgumentException)
            {
                // dictionary already contains the game
            }
        }

        public void AddMazeSolution(string name, string jsonDesc)
        {
            try
            {
                mazeSolutions.Add(name, jsonDesc);
                File.AppendAllText(solutions_path, jsonDesc + '\n');
            }
            catch (ArgumentException)
            {
                // dictionary already contains the solution
            }
        }

        public IMaze GetMaze(string name)
        {
            IMaze maze;

            if (!mazes.TryGetValue(name, out maze))
            {
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
                Console.WriteLine("MasterModel: No multiplayer game by name " + name);
                return null;
            }
            else
            {
                return game;
            }
        }

        public void RemoveMultiplayerGame(string name)
        {
            try {
                mpGames.Remove(name);
            }
            catch (ArgumentNullException)
            {
                // no such game
            }
        }

        public string GetMazeSolution(string name)
        {
            string sol;

            if (!mazeSolutions.TryGetValue(name, out sol))
            {
                //Console.WriteLine("MasterModel: No maze solution by name " + name);
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

        private void CreateDataFromFile()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            SolveAnswer solved;
            
            foreach (string line in File.ReadLines(@solutions_path))
            {
                if (line.Length > 0)
                {
                    Answer ans = serializer.Deserialize<Answer>(line);
                    solved = serializer.ConvertToType<SolveAnswer>(ans.Content);
                    mazeSolutions.Add(solved.Name, line);
                }
            }
        }
    }
}
