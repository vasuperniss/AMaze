using System;
using MazeClient.View;

namespace MazeClient.Presenter
{
    class MazeGamePresenter : IPresenter
    {
        private IIOView io;
        private IServerView server;

        public MazeGamePresenter(IIOView io, IServerView server)
        {
            this.io = io;
            this.server = server;

            this.io.SetPresenter(this);
            this.server.SetPresenter(this);
        }

        public void Run()
        {
            if (!this.server.Connect())
            {
                this.io.Display("Failed to Establish a connection with the server.");
                return;
            }
            this.io.DisplayRulesOfUse();
            while (true)
            {
                string input = this.io.GetInputFromUser();
                if (this.io.IsCloseRequest(input)) { break; }
                this.server.SendRequest(input);
            }
        }

        public void HandleRespose(string response)
        {
            // read the response which is in Json format
            // display if needed
        }
    }
}
