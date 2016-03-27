using System;

namespace MazeClient.View
{
    public delegate void HandleEvent(object sender, EventArgs args);

    interface IView
    {
        event HandleEvent OnInputReceived;
        event HandleEvent OnExitMessageReceived;
        
        void Display(string str);

        void Run();
    }
}
