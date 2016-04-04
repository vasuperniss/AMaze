using System;

namespace MazeClient.View
{
    class IO : IView
    {
        private const string END = "-1";
        private volatile bool isRunning;

        /// <summary>
        /// Occurs when [on input received].
        /// </summary>
        public event HandleEvent OnInputReceived;
        /// <summary>
        /// Occurs when [on exit message received].
        /// </summary>
        public event HandleEvent OnExitMessageReceived;

        /// <summary>
        /// Displays the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        public void Display(string str)
        {
            Console.WriteLine(str);
        }

        /// <summary>
        /// Runs this instance.
        /// Starts listenning for user input
        /// </summary>
        public void Run()
        {
            this.DisplayTermsOfUse();
            this.isRunning = true;
            string input = string.Empty;
            do
            {
                // read input from the user
                input = Console.ReadLine();
                if (this.OnInputReceived != null && input != END && isRunning)
                {
                    // raise the input received event
                    this.OnInputReceived(this, new InputEventArgs(input));
                }
            } while (input != END && this.isRunning);

            if (this.OnExitMessageReceived != null && this.isRunning)
            {
                // while stoped because of a user request
                this.OnExitMessageReceived(this, null);
            }
        }

        /// <summary>
        /// Displays the terms of use.
        /// </summary>
        private void DisplayTermsOfUse()
        {
            Console.WriteLine("To close the app, enter '-1'.");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.isRunning = false;
        }
    }
}
