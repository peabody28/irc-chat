using System.Net;

var ipPoint = new IPEndPoint(IPAddress.Any, 6667);
using var server = new Common.Server(ipPoint);

await server.Listen();

Console.ReadKey();
