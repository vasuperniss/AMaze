using MazeServer.View;

namespace MazeServer.Model.Options
{
    /// <summary>
    /// Command that is able to make changes in the model.
    /// </summary>
    abstract class Commandable
    {
        /// <summary>
        /// The model.
        /// </summary>
        protected IModel model;

        /// <summary>
        /// Executes a command described with the commandParsed array.
        /// </summary>
        /// <param name="from">the client that sent the command.</param>
        /// <param name="commandParsed">The parsed command.</param>
        public abstract void Execute(object from, string[] commandParsed);

        /// <summary>
        /// Validates the command's structure.
        /// </summary>
        /// <param name="commandParsed">The parsed command.</param>
        /// <returns>'true' if command is valid.
        ///          'false' if command is invalid.</returns>
        public abstract bool Validate(string[] commandParsed);

        /// <summary>
        /// Performs the action. Verifies the validity of the command before executing.
        /// </summary>
        /// <param name="from">the client that sent the message.</param>
        /// <param name="request">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        public void PerformAction(object from, MessageEventArgs request)
        {
            string[] commandParsed = request.Msg.Split(' ');

            if (Validate(commandParsed))
            {
                Execute(from, commandParsed);
            }
            else
            {
                // invalid option
            }
        }
    }
}
