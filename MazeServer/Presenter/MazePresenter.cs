using System.Collections.Generic;
using MazeServer.Model;
using MazeServer.Model.Options;
using MazeServer.View;
using System.Threading;

namespace MazeServer.Presenter
{
    class MazePresenter
    {
        private IModel Model;
        private ILobbyView View;
        private RequestHandler Handler;
        private List<IClientView> clients;

        public MazePresenter(IModel model, ILobbyView view)
        {
            this.Model = model;
            this.View = view;
            Handler = new RequestHandler();
            clients = new List<IClientView>();

            Handler.AddOption("generate", new GenerateMaze(Model));
            Handler.AddOption("solve", new SolveMaze(Model));
            Handler.AddOption("multiplayer", new MultiplayerMaze(Model));
            Handler.AddOption("play", new PlayMaze(Model));
            Handler.AddOption("close", new CloseMaze(Model));


            View.OnConnect += NewConnection;

            Model.ModelChanged += delegate (object reply, MessageEventArgs e)
            {
                View.SendReply(reply);
            };
        }

        public void NewConnection(object o, ConnectionEventArgs args)
        {
            clients.Add(args.CView);
            args.CView.MessageReceived += ReceivedMessage;
            Thread t = new Thread(args.CView.StartListening);
            t.Start();
        }

        public void ReceivedMessage(object o, MessageEventArgs args)
        {
            Handler.HandleRequest(args.Msg);
        }
    }
}
