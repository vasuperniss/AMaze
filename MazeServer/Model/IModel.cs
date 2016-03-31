using Maze_Library;
using Maze_Library.Maze;
using MazeServer.Presenter;
using MazeServer.View;

namespace MazeServer.Model
{
    delegate void UpdateModel(object o, MessageEventArgs e);
    interface IModel
    {
        event UpdateModel TaskCompleted;

        void AddMaze(string name, IMaze maze);

        void AddMultiplayerGame(string name, MultiplayerGame mp);

        void AddMazeSolution(string name, string jsonDesc);

        bool IsClientInQueue(object client);

        IMaze GetMaze(string name);

        MultiplayerGame GetMultiplayerGame(string name);

        string GetMazeSolution(string name);

        void CompletedTask(object from, MessageEventArgs reply);
    }
}
