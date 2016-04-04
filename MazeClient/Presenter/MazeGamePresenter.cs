using System;
using MazeClient.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using MazeClient.Model;
using MazeClient.Model.Server;
using MazeClient.Model.Answer;

namespace MazeClient.Presenter
{
    /// <summary>
    /// The Presenter class of the Maze Client
    /// </summary>
    class MazeGamePresenter
    {
        /// <summary>
        /// The io (View) to display and receive input from the user
        /// </summary>
        private IView io;
        /// <summary>
        /// The server (Model) to request and receive answers from
        /// </summary>
        private IServer server;
        /// <summary>
        /// The List of all tasks created by the Client
        /// </summary>
        private List<Task> tasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGamePresenter"/> class.
        /// </summary>
        /// <param name="io">The io View.</param>
        /// <param name="server">The server Model.</param>
        public MazeGamePresenter(IView io, IServer server)
        {
            this.io = io;
            this.server = server;
            // add the presenter to the events in the model and view
            this.io.OnInputReceived += this.HandleEvent;
            this.server.OnResponseReceived += this.HandleEvent;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HandleEvent(object sender, EventArgs args)
        {
            if (sender == this.io)
            {
                // event came from the View
                string input = (args as InputEventArgs).Input;
                // sends the request to the server using a ThreadPool
                this.tasks.Add(Task.Factory.StartNew(()
                                        => this.server.SendRequest(input)));
            }
            else if (sender == this.server)
            {
                // event came from the Server
                string responseInJson = (args as ResponseEventArgs).Response;
                if (responseInJson == null)
                {
                    this.io.Display("Server has stoped. enter any key to"
                                                          + " close the App");
                    this.io.Stop();
                    return;
                }
                IServerAnswer answer = new JsonAnswerFactory()
                                                .GetJsonAnswer(responseInJson);
                if (answer != null)
                {
                    // activate the display request on the ThreadPool
                    this.tasks.Add(Task.Factory.StartNew(()
                            => this.io.Display(answer.ToString())));
                }
                else
                {
                    // activate the display request on the ThreadPool
                    this.tasks.Add(Task.Factory.StartNew(()
                            => this.io.Display("Received a message from the"
                            + " server, but in incorrect Json format.")));
                }
            }
        }

        /// <summary>
        /// Starts up the Maze Game Client
        /// </summary>
        public void Run()
        {
            if (!this.server.Connect())
            {
                this.io.Display("Failed to connect to the Server.");
                return;
            }
            this.tasks = new List<Task>();
            // starts the io
            this.io.Run();
            // user has entered the close key
            this.server.Close();
            Task.WaitAll(tasks.ToArray());
        }
    }
}
