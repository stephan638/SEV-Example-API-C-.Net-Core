using System;

namespace SEV.TestConsole
{
    internal class Commands : BaseController
    {
        public static void Options(SEVClient sevClient = null)
        {
            SEVClient client = sevClient ?? GetSession();

            Console.ForegroundColor = CCCommands;
            Console.WriteLine($"{Environment.NewLine}Enter 1 of the following commands:{Environment.NewLine}");
            Console.WriteLine($"GetList");
            Console.WriteLine($"GetProduct /PRODUCTNUMBER");
            Console.WriteLine($"BuyProduct /PRODUCTNUMBER /QUANTITY");
            Console.WriteLine($"GetBalance");
            Console.WriteLine($"GetSessionInfo");
            Console.WriteLine($"Exit");
            Console.WriteLine();

            Console.ForegroundColor = CCDefault;
            var anwser = Console.ReadLine().ToLower().Split('/');
            ResultSeporator();

            ExecuteCommand(client, anwser);

            ResultSeporator();
            Options(client);
        }

        private static void ExecuteCommand(SEVClient client, string[] anwser) 
        {
            switch (anwser[0].Trim())
            {
                case "getlist":
                    client.GetProductList();
                    break;
                case "getproduct":
                    client.GetProduct(anwser[1].Trim());
                    break;
                case "buyproduct":
                    if (anwser.Length < 4)
                    {
                        client.BuyVoucher(anwser[1].Trim(), anwser[2]);
                    }
                    else
                    {
                        client.BuyVoucher(anwser[1].Trim(), anwser[2], anwser[3]);
                    }
                    break;
                case "getbalance":
                    client.GetCurrentBalance();
                    break;
                case "getsliptext":
                    client.GetProduct(anwser[1].Trim());
                    break;
                case "getsessioninfo":
                    client.GetLogonInformation();
                    break;
                case "exit":
                    client.Exit();
                    Environment.Exit(0);
                    return;
                default:
                    Console.ForegroundColor = CCWarning;
                    Console.WriteLine($"Invalid command, please try again.");
                    break;
            }
        }

        private static SEVClient GetSession()
        {
            return SEVClient.CreateSession();
        }
    }
}
