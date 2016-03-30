using Maze_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model
{
    class MultiplayerGame
    {
        private List<object> clients;
        private IMaze maze;
        private string name;
        private IModel model;

        public MultiplayerGame(IModel model, string name, IMaze maze)
        {
            this.name = name;
            this.maze = maze;
            this.model = model;
            clients = new List<object>();
        }

        public bool AddClient(object client)
        {
            object lockThis = new object();
            lock (lockThis)
            {
                // check if client is not already in the game
                if (!clients.Contains(client) && clients.Count < 2)
                {
                    clients.Add(client);
                    // 1 client
                    if(clients.Count == 1)
                    {
                        return true;
                    }
                    // 2 clients
                    else
        {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<object> GetClients()
        {
            return clients;
        }

        public string GetName()
        {
            return name;
        }

        public IMaze GetMaze()
        {
            return maze;
        }
    }
}
