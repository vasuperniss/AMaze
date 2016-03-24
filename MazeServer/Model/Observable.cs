using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Interfaces;

namespace MazeServer.Model
{
    abstract class Observable
    {
        List<IObserver> Observers = new List<IObserver>();

        public void AddObserver(IObserver obs)
        {
            Observers.Add(obs);
        }

        protected void NotifyObservers()
        {
            foreach (IObserver obs in Observers)
            {
                // change null to something else if there's a need for arguments
                obs.Update(this,null);
            }
        }
    }
}
