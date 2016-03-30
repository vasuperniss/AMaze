using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model.Options
{
    class CloseOption : Commandable
    {
        public CloseOption(IModel model)
        {
           this.model = model;
        }

        public override string Execute(object from, string[] commandParsed)
        {
            return null;
        }

        public override bool Validate(string[] commandParsed)
        {
            if (commandParsed.Count() != 2) return false;

            return true;
        }
    }
}
