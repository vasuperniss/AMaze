using System.Web.Script.Serialization;

namespace MazeServer.Model.JsonOptions
{
    /// <summary>
    /// 
    /// </summary>
    class Answer
    {
        /// <summary>
        /// The type of command.
        /// </summary>
        private int type;
        /// <summary>
        /// The data of the command.
        /// </summary>
        private object data;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public int Type
        {
            get { return type; }
            set { type = value;  }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public object Content
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Generates a json answer.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="ans">The answer.</param>
        /// <returns>
        /// json serialization of the class.
        /// </returns>
        public string GetJSONAnswer(int type, IServerAnswer ans)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            this.type = type;
            this.data = ans;

            return serializer.Serialize(this);
        }
    }
}
