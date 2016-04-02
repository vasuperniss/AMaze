using MazeClient.Model.Answer;
using System;
using System.Web.Script.Serialization;

namespace MazeClient.Model
{
    class JsonAnswerFactory
    {
        private const int GENERATE = 1;
        private const int SOLVE = 2;
        private const int MULTIPLAYER = 3;
        private const int PLAY = 4;

        public IServerAnswer GetJsonAnswer(string jsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try {
                ServerAnswer answer = serializer.Deserialize<ServerAnswer>(jsonStr);
                switch (answer.Type)
                {
                    case GENERATE:
                        return serializer.ConvertToType<GenerateAnswer>(answer.Content);
                    case SOLVE:
                        return serializer.ConvertToType<SolveAnswer>(answer.Content);
                    case MULTIPLAYER:
                        return serializer.ConvertToType<MultiplayerAnswer>(answer.Content);
                    case PLAY:
                        return serializer.ConvertToType<PlayAnswer>(answer.Content);
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    internal class ServerAnswer
    {
        private int type;
        private object content;

        public int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public object Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
    }
}
