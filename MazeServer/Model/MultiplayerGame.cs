using Maze_Library;
using Maze_Library.Maze;
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
        private Socket FirstPlayer;
        private Socket SecondPlayer;
        private IMaze Maze;
        private string Name;

        public MultiplayerGame(Socket firstPlayer, Socket secondPlayer, string name)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer= secondPlayer;
            Name = name;
            // Maze = GenerateMaze(name + " maze", 1);
        }

        public Socket GetFirstPlayerSocket()
        {
            return FirstPlayer;
        }

        public Socket GetSecondPlayerSocket()
        {
            return SecondPlayer;
        }

        public string GetName()
        {
            return Name;
        }

        public IMaze GetMaze()
        {
            return Maze;
        }
    }
}
