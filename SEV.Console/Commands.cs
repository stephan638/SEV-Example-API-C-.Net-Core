using System;

namespace SEV.TestConsole
{
    internal class Commands : BaseController
    {
        private readonly Execute _execute;

        public Commands()
        {
            _execute = new Execute();
        }

        public void Options()
        {
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

            switch (anwser[0].Trim())
            {
                case "getlist":
                    _execute.GetProductList();
                    break;
                case "getproduct":
                    _execute.GetProduct(anwser[1].Trim());
                    break;
                case "buyproduct":
                    if (anwser.Length < 4)
                    {
                        _execute.BuyVoucher(anwser[1].Trim(), anwser[2]);
                    }
                    else
                    {
                        _execute.BuyVoucher(anwser[1].Trim(), anwser[2], anwser[3]);
                    }
                    break;
                case "getbalance":
                    _execute.GetCurrentBalance();
                    break;
                case "getsliptext":
                    _execute.GetProduct(anwser[1].Trim());
                    break;
                case "getsessioninfo":
                    _execute.GetLogonInformation();
                    break;
                case "exit":
                    _execute.Exit();
                    return;
                default:
                    Console.ForegroundColor = CCWarning;
                    Console.WriteLine($"Invalid command, please try again.");
                    break;
            }

            ResultSeporator();
            Options();
        }
    }
}
