using System;

namespace SEV.TestConsole
{
    class Program : BaseController
    {
        private static void Main()
        {
            Console.ForegroundColor = CCInfo;
            Console.WriteLine($"{Environment.NewLine}SEV Client API Example Code C# .Net Core{Environment.NewLine}");
            ActionSeporator();
            Console.ForegroundColor = CCInfo;
            Console.WriteLine("See our API documentation website on URL:");
            Console.WriteLine("https://sev-api.allservices.nl");
            Console.ForegroundColor = CCDefault;
            ActionSeporator();

            new Commands().Options();
        }
    }
}