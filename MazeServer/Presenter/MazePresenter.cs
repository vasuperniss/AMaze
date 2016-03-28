using System.Collections.Generic;
using MazeServer.Model;
using MazeServer.Model.Options;
using MazeServer.View;
using System.Threading;
using System.Threading.Tasks;

namespace MazeServer.Presenter
{
    class MazePresenter
    {
        private IModel Model;
        private ILobbyView View;
        private RequestHandler Handler;
        private List<IClientView> clients;
        private List<Task> tasks;

        public MazePresenter(IModel model, ILobbyView view)
        {
            Model = model;
            View = view;
            Handler = new RequestHandler();
            clients = new List<IClientView>();
            tasks = new List<Task>();

            Handler.AddOption("generate", new GenerateMaze(Model));
            Handler.AddOption("solve", new SolveMaze(Model));
            Handler.AddOption("multiplayer", new MultiplayerMaze(Model));
            Handler.AddOption("play", new PlayMaze(Model));
            Handler.AddOption("close", new CloseMaze(Model));

            View.OnConnect += NewConnection;
            Model.TaskCompleted += ReplyToClient;
        }

        public void NewConnection(object o, ConnectionEventArgs args)
        {
            clients.Add(args.CView);
            args.CView.MessageReceived += MessageFromClient;
            Thread t = new Thread(args.CView.StartListening);
            t.Start();
        }

        public void MessageFromClient(object o, MessageEventArgs args)
        {
            Handler.HandleRequest(args);
            //tasks.Add(Task.Factory.StartNew(Handler.HandleRequest(args)));
        }

        public void ReplyToClient(object o, MessageEventArgs args)
        {
            args.Client.SendMessage(args.Msg);
        }
    }
}
