using MazeServer.Model;
using MazeServer.Model.Options;
using MazeServer.View;
using System.Threading;
using System.Threading.Tasks;

namespace MazeServer.Presenter
{
    /// <summary>
    /// Presenter. Handles notifications from model and view.
    /// </summary>
    class MazePresenter
    {
        /// <summary>
        /// The model.
        /// </summary>
        private IModel Model;
        /// <summary>
        /// The view.
        /// </summary>
        private ILobbyView View;
        /// <summary>
        /// The request handler.
        /// </summary>
        private RequestHandler Handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazePresenter"/> class.
        /// Initializes a request handler, adds the options of the server, 
        /// and adds the NewConnection and ReplyToClient methods to the delegate of the view and model respectively.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="view">The view.</param>
        public MazePresenter(IModel model, ILobbyView view)
        {
            Model = model;
            View = view;
            Handler = new RequestHandler();

            Handler.AddOption("generate", new GenerateOption(Model));
            Handler.AddOption("solve", new SolveOption(Model));
            Handler.AddOption("multiplayer", new MultiplayerOption(Model));
            Handler.AddOption("play", new PlayOption(Model));
            Handler.AddOption("close", new CloseOption(Model));

            View.OnConnect += NewConnection;
            Model.TaskCompleted += ReplyToClient;
        }

        /// <summary>
        /// Upon a new connection, the MessageFromClient() method is added to the clients delegate.
        /// </summary>
        /// <param name="sender">The ILobbyView.</param>
        /// <param name="args">The <see cref="ConnectionEventArgs"/> instance containing the event data.</param>
        public void NewConnection(object sender, ConnectionEventArgs args)
        {
            args.CView.MessageReceived += MessageFromClient;
            Thread t = new Thread(args.CView.StartListening);
            t.Start();
        }

        /// <summary>
        /// Upon receiving a message from a client a new thread is opened for handling his request.
        /// </summary>
        /// <param name="sender">The clientHandler that send the notification.</param>
        /// <param name="args">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        public void MessageFromClient(object sender, MessageEventArgs args)
        {
            Task.Factory.StartNew(() => Handler.HandleRequest(sender, args));
        }

        /// <summary>
        /// Sends a message to a client. If message or client are null nothing is done.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="args">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        public void ReplyToClient(object from, MessageEventArgs args)
        {
            // if message is null then reply is not sent to client
            IClientView client = from as IClientView;

            if (client == null) return;
            if (args.Msg != null) client.SendMessage(args.Msg);
        }
    }
}
