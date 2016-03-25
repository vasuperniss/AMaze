using System;
using MazeClient.Presenter;

namespace MazeClient.View
{
    class IO : IIOView
    {
        private IPresenter presenter;

        public void Display(string str)
        {
            Console.WriteLine(str);
        }

        public string GetInputFromUser()
        {
            return Console.ReadLine();
        }

        public void SetPresenter(IPresenter presenter)
        {
            this.presenter = presenter;
        }
    }
}
