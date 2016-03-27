using System;

namespace MazeClient.Model.Server
{
    class ResponseEventArgs : EventArgs
    {
        private string response;

        public ResponseEventArgs(string response)
        {
            this.response = response;
        }

        public string Response
        {
            get { return this.response; }
        }
    }
}
