using Maze_Library;
using MazeServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model
{
    interface IMazeModel
    {
        event UpdateModel ModelChanged;

        void AddMaze(string name, IMaze maze);

        void AddMultiplayerGame(string name, MultiplayerGame mp);

        IMaze GetMaze(string name);

        MultiplayerGame GetMultiplayerGame(string name);

        void CompletedTask(string reply);
    }
}
