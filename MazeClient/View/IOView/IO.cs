using System;

namespace MazeClient.View
{
    class IO : IIOView
    {
        public void Display(string str)
        {
            Console.WriteLine(str);
        }

        public string GetInputFromUser()
        {
            return Console.ReadLine();
        }
    }
}
