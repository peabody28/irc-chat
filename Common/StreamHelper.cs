using System.Net.Sockets;
using System.Text;

namespace Common
{
    public class StreamHelper
    {
        public static async Task<string> GetData(Socket? clientSocket)
        {
            string data = string.Empty;
            while (true)
            {
                var buffer = new byte[512];
                var received = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
                data += Encoding.ASCII.GetString(buffer, 0, received);

                var indexOfEnd = data.IndexOf("\r\n");
                if (indexOfEnd > -1)
                {
                    data = data.Substring(0, indexOfEnd);
                    break;
                }
            }

            return data;
        }
    }
}
