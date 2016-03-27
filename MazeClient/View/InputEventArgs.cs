using System;

namespace MazeClient.View
{
    class InputEventArgs : EventArgs
    {
        private string input;

        public InputEventArgs(string input)
        {
            this.input = input;
        }

        public string Input
        {
            get { return this.input; }
        }
    }
}
