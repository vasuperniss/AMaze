using Maze_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Interfaces
{
    interface IMazeModel
    {
        void GenerateMaze(string name, int type);

        void CreateMultiplayerGame(string name);

        IMaze GetMaze(string name);

        //MultiplayerGame GetMpGame(string name);

        void AddObserver();
    }
}
