using Maze_Library.Maze;
using MazeServer.Model.JsonOptions;
using MazeServer.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace MazeServer.Model
{
    /// <summary>
    /// Model of the server that handles and contains data.
    /// </summary>
    /// <seealso cref="MazeServer.Model.IModel" />
    class MasterModel : IModel
    {
        /// <summary>
        /// The mazes dictionary(by name).
        /// </summary>
        private Dictionary<string, IMaze> mazes;
        /// <summary>
        /// The maze solutions dictionary(by name).
        /// </summary>
        private Dictionary<string, string> mazeSolutions;
        /// <summary>
        /// The mp games dictionary(by name).
        /// </summary>
        private Dictionary<string, MultiplayerGame> mpGames;
        /// <summary>
        /// The path to the solution file.
        /// </summary>
        private string solutions_path;
        /// <summary>
        /// Represents an event that is raised when a task either successfully or unsuccessfully completes.
        /// </summary>
        public event UpdateModel TaskCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterModel"/> class.
        /// initializes the dictionaries, and creates a path for the solutions file.
        /// Attemps to read from the file(if it exists).
        /// </summary>
        public MasterModel()
        {
            mazes = new Dictionary<string, IMaze>();
            mazeSolutions = new Dictionary<string, string>();
            mpGames = new Dictionary<string, MultiplayerGame>();

            string dir = Directory.GetCurrentDirectory();
            solutions_path = dir + "\\" + "mazeSolutions.json";

            CreateDataFromFile();
        }

        /// <summary>
        /// Adds a maze.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="maze">The maze.</param>
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

        /// <summary>
        /// Adds a multiplayer game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="mp">The multiplayer game.</param>
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

        /// <summary>
        /// Adds a maze solution.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <param name="jsonDesc">The json description of the solution.</param>
        public void AddMazeSolution(string name, string jsonDesc)
        {
            try
            {
                mazeSolutions.Add(name, jsonDesc);
                File.AppendAllText(@solutions_path, jsonDesc + '\n');
            }
            catch (ArgumentException)
            {
                // dictionary already contains the solution
            }
        }

        /// <summary>
        /// Gets a maze.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <returns>the maze if it exists.
        ///          null if it doesn't. </returns>
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

        /// <summary>
        /// Gets a multiplayer game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <returns>the game if it exists.
        ///          null if it doesn't. </returns>
        public MultiplayerGame GetMultiplayerGame(string name)
        {
            MultiplayerGame game;

            if (!mpGames.TryGetValue(name, out game))
            {
                return null;
            }
            else
            {
                return game;
            }
        }

        /// <summary>
        /// Removes a multiplayer game.
        /// </summary>
        /// <param name="name">The name of the game.</param>
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

        /// <summary>
        /// Gets a maze solution.
        /// </summary>
        /// <param name="name">The name of the maze.</param>
        /// <returns> the solution if it exists.
        ///           null if it doesn't. </returns>
        public string GetMazeSolution(string name)
        {
            string sol;

            if (!mazeSolutions.TryGetValue(name, out sol))
            {
                return null;
            }
            else
            {
                return sol;
            }
        }

        /// <summary>
        /// Determines whether a given client is in a multiplayer game(or waiting to play).
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns> the game if the client is in a game. 
        ///           null if he isn't. </returns>
        public MultiplayerGame IsClientInGame(object client)
        {
            foreach (string key in mpGames.Keys)
            {
                if (mpGames[key].ContainsClient(client)) return mpGames[key];
            }

            return null;
        }

        /// <summary>
        /// Notifies the presenter that a task has been completed.
        /// </summary>
        /// <param name="from">the client that will receive an answer from the server.</param>
        /// <param name="reply">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        public void CompletedTask(object from, MessageEventArgs reply)
        {
            TaskCompleted(from, reply);
        }

        /// <summary>
        /// Attemps to read the solutions from a file.
        /// If the file doesn't exist it is created, or if the file is empty the function exists.
        /// </summary>
        private void CreateDataFromFile()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            SolveAnswer solved;

            if (!File.Exists(@solutions_path))
            {
                return;
            }

            foreach (string line in File.ReadLines(@solutions_path))
            {
                if (line.Length > 0)
                {
                    Answer ans = serializer.Deserialize<Answer>(line);
                    solved = serializer.ConvertToType<SolveAnswer>(ans.Content);
                    try
                    {
                        mazeSolutions.Add(solved.Name, line);
                    }
                    catch (Exception)
                    {
                        // Unexpected description of solution, or a solution by that name already exists.
                    }
                }
            }
        }
    }
}
