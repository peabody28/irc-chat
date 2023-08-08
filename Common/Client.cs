using System.Net.Sockets;
using System.Text;

namespace Common
{
    public abstract class Client : IClient, IDisposable
    {
        public Socket socket;

        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async void SendMessage(string msg)
        {
            if (!msg.EndsWith("\r\n"))
                msg += "\r\n";

            var messageBytes = Encoding.UTF8.GetBytes(msg);
            await socket.SendAsync(messageBytes, SocketFlags.None);
        }

        public void Dispose()
        {
            socket.Close();
        }
    }
}
