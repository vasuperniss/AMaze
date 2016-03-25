using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeClient.View
{
    interface IIOView
    {
        void Display(string str);

        string GetInputFromUser();
    }
}
