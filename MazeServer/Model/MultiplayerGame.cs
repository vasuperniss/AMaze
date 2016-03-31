using Maze_Library;
using System;
using System.Collections.Generic;
using Maze_Library.Maze;
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
        private object lockThis;

        public MultiplayerGame(IModel model, string name, IMaze maze)
        {
            this.name = name;
            this.maze = maze;
            this.model = model;
            lockThis = new object();
            clients = new List<object>();
        }

        public bool AddClient(object client)
        {
            
            lock (lockThis)
            {
                // check if client is not already in the game
                if (!clients.Contains(client) && clients.Count < 2)
                {
                    clients.Add(client);
                    return true;
                }
            }
            return false;
        }

        public bool ContainsClient(object cl)
        {
            return clients.Contains(cl);
        }

        public List<object> GetClients()
        {
            return clients;
        }

        public void RetrieveOtherClient(object cl, out object other)
        {
            other = null;
            foreach (object client in clients)
            {
                if (client != cl) other = client;
            }
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
