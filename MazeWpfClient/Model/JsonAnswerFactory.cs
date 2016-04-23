using MazeClient.Model.Answer;
using System;
using System.Web.Script.Serialization;

namespace MazeClient.Model
{
    /// <summary>
    /// Factory for generating ServerAnswers from json strings
    /// </summary>
    class JsonAnswerFactory
    {
        /// <summary>
        /// The generate option code
        /// </summary>
        private const int GENERATE = 1;
        /// <summary>
        /// The solve option code
        /// </summary>
        private const int SOLVE = 2;
        /// <summary>
        /// The multiplayer option code
        /// </summary>
        private const int MULTIPLAYER = 3;
        /// <summary>
        /// The play option code
        /// </summary>
        private const int PLAY = 4;

        /// <summary>
        /// Gets the ServerAnswer object based on the json string.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        /// <returns>the ServerAnswer</returns>
        public IServerAnswer GetJsonAnswer(string jsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try {
                ServerAnswer answer = serializer.Deserialize<ServerAnswer>(jsonStr);
                switch (answer.Type)
                {
                    case GENERATE:
                        answer.Content = serializer.ConvertToType<GenerateAnswer>(answer.Content);
                        break;
                    case SOLVE:
                        answer.Content = serializer.ConvertToType<SolveAnswer>(answer.Content);
                        break;
                    case MULTIPLAYER:
                        answer.Content = serializer.ConvertToType<MultiplayerAnswer>(answer.Content);
                        break;
                    case PLAY:
                        answer.Content = serializer.ConvertToType<PlayAnswer>(answer.Content);
                        break;
                    default:
                        // unknown answer type
                        return null;
                }
                answer.JsonStr = jsonStr;
                return answer;
            }
            catch (Exception)
            {
                // incorrect json format
                return null;
            }
        }
    }

    internal class ServerAnswer : IServerAnswer
    {
        /// <summary>
        /// The type of Answer
        /// </summary>
        private int type;
        /// <summary>
        /// The content of the Answer
        /// </summary>
        private object content;
        /// <summary>
        /// The json string of the answer
        /// </summary>
        private string jsonStr;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public object Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        /// <summary>
        /// Gets or sets the json string.
        /// </summary>
        /// <value>
        /// The json string.
        /// </value>
        public string JsonStr
        {
            get { return this.jsonStr; }
            set { this.jsonStr = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string str = this.jsonStr;
            str += "\n" + this.Content.ToString();
            return str;
        }
    }
}
