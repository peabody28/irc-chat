using System.Net.Sockets;
using System.Net;
using System.Text;

var ipPoint = new IPEndPoint(IPAddress.Any, 6667);
using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(ipPoint);

socket.Listen(1000);

byte[] bytes = null;
string str = string.Empty;

while (true)
{
    using var client = await socket.AcceptAsync();
    var command = await GetData(client);
    Console.WriteLine(command);
}

async Task<string> GetData(Socket? socket)
{
    string data = string.Empty;
    while (true)
    {
        var buffer = new byte[512];
        var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
        data += Encoding.ASCII.GetString(buffer, 0, received);

        var eom = "end";
        if (data.IndexOf(eom) > -1)
        {
            var ackMessage = "<|ACK|>";
            var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
            await socket.SendAsync(echoBytes, 0);

            break;
        }
    }

    return data;
}