using MazeClient.Model.Answer;
using System.Web.Script.Serialization;

namespace MazeClient.Model
{
    class JsonAnswerFactory
    {
        public IServerAnswer GetJsonAnswer(string jsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //IServerAnswer result = serializer.Deserialize<>

            return null;
        }
    }
}
