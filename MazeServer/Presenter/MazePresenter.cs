using MazeServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Model;

namespace MazeServer.Presenter
{
    class MazePresenter: IObserver
    {
        private IMazeModel Model;
        private IMazeView View;

        public MazePresenter(IMazeModel model, IMazeView view)
        {
            this.Model = model;
            this.View = view;
        }

        public void Update(Observable observable, object p)
        {
            if(observable == View)
            {
                string receivedMessage = View.GetMessage();
                RequestHandler handler = new RequestHandler();

                handler.HandleRequest(receivedMessage);
            }

            if(observable == Model)
            {

            }
        }
    }
}
