using System;

namespace MazeServer.View
{

    /// <summary>
    /// Arguments for when a client connects to the server.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// The ClientView.
        /// </summary>
        private IClientView view;

        /// <summary>
        /// Gets the ClientView.
        /// </summary>
        /// <value>
        /// The ClientView.
        /// </value>
        public IClientView CView
        {
            get { return view; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionEventArgs"/> class.
        /// </summary>
        /// <param name="cview">The clientView.</param>
        public ConnectionEventArgs(IClientView cview)
        {
            view = cview;
        }
    }
}
