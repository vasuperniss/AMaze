using MazeClient.Presenter;

namespace MazeClient.View
{
    interface IServerView
    {
        bool Connect();

        void Close();

        void SetPresenter(IPresenter presenter);

        void SendRequest(string request);
    }
}
