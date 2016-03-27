using MazeServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Model;
using MazeServer.Model.Options;
using MazeServer.View;

namespace MazeServer.Presenter
{
    class MazePresenter
    {
        private IMazeModel Model;
        private IMazeView View;
        private RequestHandler Handler;

        public MazePresenter(IMazeModel model, IMazeView view)
        {
            this.Model = model;
            this.View = view;
            Handler = new RequestHandler();

            Handler.AddOption("generate", new GenerateMaze(Model));
            Handler.AddOption("solve", new SolveMaze(Model));
            Handler.AddOption("multiplayer", new MultiplayerMaze(Model));
            Handler.AddOption("play", new PlayMaze(Model));
            Handler.AddOption("close", new CloseMaze(Model));

            View.ViewChanged += delegate (object o, EventArgs e)
            {
                string message = View.GetMessage();
                Handler.HandleRequest(message);
            };

            Model.ModelChanged += delegate (object o, EventArgs e)
            {
                string reply = Model.GetServerReply();
                View.SendReply(reply);
            };
        }
    }
}
