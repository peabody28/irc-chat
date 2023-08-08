namespace Common
{
    public interface IClient : IDisposable
    {
        void SendMessage(string msg);
    }
}
