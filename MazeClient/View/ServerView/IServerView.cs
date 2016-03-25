using MazeClient.Presenter;

namespace MazeClient.View
{
    interface IServerView
    {
        bool Connect();

        void Close();

        void AddPresenter(IPresenter presenter);

        void SendRequest(string request);
    }
}
