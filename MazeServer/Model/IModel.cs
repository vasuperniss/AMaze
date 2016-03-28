using Maze_Library;
using MazeServer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model
{
    delegate void UpdateModel(object o, MessageEventArgs e);
    interface IModel
    {
        event UpdateModel ModelChanged;

        void AddMaze(string name, IMaze maze);

        void AddMultiplayerGame(string name, MultiplayerGame mp);

        IMaze GetMaze(string name);

        MultiplayerGame GetMultiplayerGame(string name);

        void CompletedTask(string reply);
    }
}
