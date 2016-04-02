using System;

namespace MazeClient.View
{
    class IO : IView
    {
        private const string END = "-1";
        private volatile bool isRunning;

        public event HandleEvent OnInputReceived;
        public event HandleEvent OnExitMessageReceived;

        public void Display(string str)
        {
            Console.WriteLine(str);
        }

        public void Run()
        {
            this.DisplayTermsOfUse();
            this.isRunning = true;
            string input = string.Empty;
            do
            {
                input = Console.ReadLine();
                if (this.OnInputReceived != null && input != END && isRunning)
                {
                    this.OnInputReceived(this, new InputEventArgs(input));
                }
            } while (input != END && this.isRunning);

            if (this.OnExitMessageReceived != null && this.isRunning)
            {
                this.OnExitMessageReceived(this, null);
            }
        }

        private void DisplayTermsOfUse()
        {
            Console.WriteLine("To close the app, enter '-1'.");
            Console.WriteLine("Would you like to have a more visual"
                            + " version ? (y/n)");
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                AppSettings.Settings["isCoolVersion"] = "true";
            }
        }

        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
