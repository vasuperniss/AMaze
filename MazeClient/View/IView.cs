using System;

namespace MazeClient.View
{
    /// <summary>
    /// delegate for basic event functions
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
    public delegate void HandleEvent(object sender, EventArgs args);

    /// <summary>
    /// View Interface to be used by the Presenter
    /// </summary>
    interface IView
    {
        /// <summary>
        /// Occurs when [on input received].
        /// </summary>
        event HandleEvent OnInputReceived;
        /// <summary>
        /// Occurs when [on exit message received].
        /// </summary>
        event HandleEvent OnExitMessageReceived;

        /// <summary>
        /// Displays the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        void Display(string str);

        /// <summary>
        /// Runs this instance.
        /// Starts listenning for user input
        /// </summary>
        void Run();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}
