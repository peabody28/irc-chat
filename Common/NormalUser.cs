namespace Common
{
    public class NormalUser : Client
    {
        public NormalUser(string host, int port) : base() 
        {
            socket.ConnectAsync(host, port);
        }
    }
}
