using MazeClient.Presenter;

namespace MazeClient.View
{
    interface IIOView
    {
        void Display(string str);

        string GetInputFromUser();

        void SetPresenter(IPresenter presenter);
    }
}
