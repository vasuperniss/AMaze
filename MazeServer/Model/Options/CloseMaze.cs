using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model.Options
{
    class CloseMaze : Commandable
    {
        public CloseMaze(IModel model)
        {
            Model = model;
        }

        public override string Execute()
        {
            return "close";
        }

        public override bool Validate()
        {
            if (CommandParsed.Count() != 2) return false;

            return true;
        }
    }
}
