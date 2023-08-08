using System.Net;
using System.Net.Sockets;

namespace Common
{
    public class Server : Client
    {
        public Action<Socket> Handler { get; set; }

        private List<User> users = new List<User>();

        public Server(IPEndPoint ipPoint) : base()
        {
            socket.Bind(ipPoint);
            socket.Listen(1000);
        }

        public async Task Listen()
        {
            var threads = new List<Thread>();
            while (true)
            {
                var clientSocket = await socket.AcceptAsync();
                users.Add(new User { Socket = clientSocket });

                var th = new Thread(() => HandleRequest(clientSocket));
                th.Start();
                threads.Add(th);

                Console.WriteLine(threads.Count);
            }
        }

        public void HandleRequest(Socket clientSocket)
        {
            while (true) // session loop
            {
                try
                {
                    var request = StreamHelper.GetData(clientSocket).Result;
                    var command = CommandHelper.GetCommand(request);
                    var arguments = CommandHelper.GetArguments(request);

                    var user = users.FirstOrDefault(u => u.Socket.Equals(clientSocket));
                    Console.WriteLine($"User: {user.Name} sends request: {request}");
                    /*
                     * PASS
                     * NICK
                     * USER
                    */

                    if (command == "/pass")
                    {
                        user.PasswordHash = Md5Helper.Hash(arguments.First());
                    }
                    else if (command == "/nick")
                    {
                        user.Name = arguments.First();
                    }
                    else if (command == "/user")
                    {
                        user.Name = arguments.First();
                    }
                    else if(command == "/msg")
                    {
                        var targetUser = users.FirstOrDefault(u => u.Name.Equals(arguments.ElementAt(0)));
                        targetUser.SendMessage(arguments.ElementAtOrDefault(1));
                    }
                }
                catch
                {
                    // client disconnected
                    break;
                }
            }
        }
    }
}