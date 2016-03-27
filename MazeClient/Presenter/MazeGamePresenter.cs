using System;
using MazeClient.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using MazeClient.Model;
using MazeClient.Model.Server;
using MazeClient.Model.Answer;

namespace MazeClient.Presenter
{
    class MazeGamePresenter
    {
        private IView io;
        private IServer server;

        private List<Task> tasks;

        public MazeGamePresenter(IView io, IServer server)
        {
            this.io = io;
            this.server = server;

            this.io.OnInputReceived += this.HandleEvent;
        }

        private void HandleEvent(object sender, EventArgs args)
        {
            if (sender == this.io)
            {
                string input = (args as InputEventArgs).Input;
                this.server.SendRequest(input);
                this.tasks.Add(Task.Factory.StartNew(()
                                        => this.server.SendRequest(input)));
            }
            else if (sender == this.server)
            {
                string responseInJson = (args as ResponseEventArgs).Response;
                IServerAnswer answer = new JsonAnswerFactory()
                                                .GetJsonAnswer(responseInJson);
                this.tasks.Add(Task.Factory.StartNew(()
                        => this.io.Display(answer.GetStringRepresentation())));
            }
        }

        public void Run()
        {
            if (!this.server.Connect())
            {
                this.io.Display("Failed to connect to the Server.");
                return;
            }
            this.tasks = new List<Task>();
            this.io.Run();
            Task.WaitAll(tasks.ToArray());
        }
    }
}
