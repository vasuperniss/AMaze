using System.Collections.Generic;
using MazeServer.Model;
using MazeServer.Model.Options;
using MazeServer.View;
using System.Threading;
using System.Threading.Tasks;
using System;

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

            Handler.AddOption("generate", new GenerateOption(Model));
            Handler.AddOption("solve", new SolveOption(Model));
            Handler.AddOption("multiplayer", new MultiplayerOption(Model));
            Handler.AddOption("play", new PlayOption(Model));
            Handler.AddOption("close", new CloseOption(Model));

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

        public void MessageFromClient(object sender, MessageEventArgs args)
        {
            tasks.Add(Task.Factory.StartNew(() => Handler.HandleRequest(sender, args)));
        }

        public void ReplyToClient(object from, MessageEventArgs args)
        {
            // if message is null then reply is not sent to client
            IClientView client = from as IClientView;

            if (client == null) return;
            if (args.Msg != null) client.SendMessage(args.Msg);
        }
    }
}
