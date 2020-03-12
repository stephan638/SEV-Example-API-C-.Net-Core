using System;

namespace SEV.TestConsole
{
    internal class BaseController
    {
        public const ConsoleColor CCDefault = ConsoleColor.White;
        public const ConsoleColor CCError = ConsoleColor.Red;
        public const ConsoleColor CCWarning = ConsoleColor.Yellow;
        public const ConsoleColor CCSuccesful = ConsoleColor.Green;
        public const ConsoleColor CCInfo = ConsoleColor.Cyan;
        public const ConsoleColor CCCommands = ConsoleColor.Gray;

        public static void ActionSeporator()
        {
            Console.WriteLine("****************************************************************************"
                              , Console.ForegroundColor = CCDefault);
        }

        public static void ResultSeporator()
        {
            Console.WriteLine("----------------------------------------------------------------------------"
                              , Console.ForegroundColor = CCDefault);
        }
    }
}
