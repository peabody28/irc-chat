using System.Net.Sockets;
using System.Text;

namespace Common
{
    public class User
    {
        public Socket Socket { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public async void SendMessage(string msg)
        {
            if (!msg.EndsWith("\r\n"))
                msg += "\r\n";

            var messageBytes = Encoding.UTF8.GetBytes(msg);
            await Socket.SendAsync(messageBytes, SocketFlags.None);
        }

        public async Task<string> GetMessage()
        {
            return await StreamHelper.GetData(Socket);
        }
    }
}
