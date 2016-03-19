using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Library.Algorithms
{
    class StateComparer<T> : IComparer<State<T>>
    {
        public int Compare(State<T> x, State<T> y)
        {
            if (x.Cost > y.Cost)
            {
                return 1;
            }
            else if (x.Cost == y.Cost)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
