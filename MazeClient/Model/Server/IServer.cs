using System;

namespace MazeClient.Model.Server
{
    public delegate void HandleEvent(object sender, EventArgs args);

    interface IServer
    {
        event HandleEvent OnResponseReceived;

        bool Connect();

        void Close();

        void SendRequest(string request);
    }
}
