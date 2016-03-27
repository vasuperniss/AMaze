using System;

namespace MazeClient.View
{
    class IO : IView
    {
        private const string END = "-1";

        public event HandleEvent OnInputReceived;
        public event HandleEvent OnExitMessageReceived;

        public void Display(string str)
        {
            Console.WriteLine(str);
        }

        public void Run()
        {
            Console.WriteLine("to close the app, enter '-1'.");
            string input = string.Empty;
            do
            {
                input = Console.ReadLine();
                if (this.OnInputReceived != null && input != END)
                {
                    this.OnInputReceived(this, new InputEventArgs(input));
                }
            } while (input != END);

            if (this.OnExitMessageReceived != null)
            {   
                this.OnExitMessageReceived(this, null);
            }
        }
    }
}
