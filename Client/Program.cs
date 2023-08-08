
using Common;
using System.Net.Sockets;

var port = 6667;
var url = "localhost";

using var client = new NormalUser(url, port);

var thread = new Thread(async () =>
{
    while (true)
    {
        if (client.socket.Connected)
        {
            var str = await StreamHelper.GetData(client.socket);
            Console.WriteLine("Message recieved: " + str);
        }
    }
});
thread.Start();

while (true)
{
    Console.WriteLine($"input message:");
    var message = Console.ReadLine();
    client.SendMessage(message);
    Console.WriteLine("Sended!");
}