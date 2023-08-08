namespace Common
{
    public class CommandHelper
    {

        public static string GetCommand(string request)
        {
            return request.Split()[0];
        }

        public static IEnumerable<string> GetArguments(string request)
        {
            return request.Split(" ").Skip(1);
        }
    }
}
