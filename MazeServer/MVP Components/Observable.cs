using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.MVP_Components.Interfaces
{
    abstract class Observable
    {
        List<Observer> Observers = new List<Observer>();

        void AddObserver(Observer obs)
        {
            Observers.Add(obs);
        }

        void NotifyObservers()
        {
            foreach (Observer obs in Observers)
            {
                // change null to something else if there's a need for arguments
                obs.Update(this,null);
            }
        }
    }
}
