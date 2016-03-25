using System;
using MazeClient.Presenter;

namespace MazeClient.View
{
    class IO : IIOView
    {
        private const string END = "-1";

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

        public bool IsCloseRequest(string input)
        {
            if (input == END) { return true; }
            return false;
        }

        public void DisplayRulesOfUse()
        {
            Console.WriteLine("to close the app, enter '-1'.");
        }
    }
}
