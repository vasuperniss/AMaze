using System;

namespace MazeClient.View
{
    /// <summary>
    /// Event for user Input
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class InputEventArgs : EventArgs
    {
        /// <summary>
        /// The input received from the user
        /// </summary>
        private string input;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputEventArgs"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        public InputEventArgs(string input)
        {
            this.input = input;
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public string Input
        {
            get { return this.input; }
        }
    }
}
