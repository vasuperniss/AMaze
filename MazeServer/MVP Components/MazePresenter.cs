using MazeServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.MVP_Components
{
    class MazePresenter
    {
        private IMazeModel Model;
        private IMazeView View;

        public MazePresenter(IMazeModel model, IMazeView view)
        {
            this.Model = model;
            this.View = view;
        }
    }
}
