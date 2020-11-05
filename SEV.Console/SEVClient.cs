using Microsoft.Extensions.Configuration;
using SEV.Client.Config;
using System;
using System.Linq;
using System.Text;

namespace SEV.TestConsole
{
    internal class SEVClient : BaseController
    {
        private readonly string _userName = "";
        private readonly string _password = "";

        private static SessionConfig session;

        private SEVClient(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public static SEVClient CreateSession(bool firstTry = true)
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("SEV.TestConsole.json", optional: true, reloadOnChange: true)
                    .Build();

            var sevClient = new SEVClient(configuration["UserName"], configuration["Password"]);

            if (!string.IsNullOrEmpty(sevClient._userName) && !string.IsNullOrEmpty(sevClient._password) && firstTry)
            {
                Console.WriteLine($"Start login for user { sevClient._userName }");
                session = SessionConfig.GetSessionConfigAsync(sevClient._userName, sevClient._password).Result;
            }
            else
            {
                Console.WriteLine("Enter your API user name:");
                string userName = Console.ReadLine();
                Console.WriteLine("Enter your API password:");

                string password = null;
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    password += key.KeyChar;
                }

                session = SessionConfig.GetSessionConfigAsync(userName, password).Result;
            }

            if (!session.LoginSuccessful)
            {
                ActionSeporator();
                Console.ForegroundColor = CCWarning;
                Console.WriteLine("Login failed incorrect user name or password!");
                Console.WriteLine("Please try again.");
                ActionSeporator();
                session = null;
                CreateSession(false);
                return sevClient;
            }

            ActionSeporator();
            Console.ForegroundColor = CCSuccesful;
            Console.WriteLine("Login successfully");
            ActionSeporator();

            return sevClient;
        }

        public void GetLogonInformation()
        {
            Console.ForegroundColor = CCInfo;
            var sb = new StringBuilder();
            sb.AppendLine($"Connected to URL         :{ session.BaseUrl }");
            sb.AppendLine($"Login as                 :{ session.BearerToken.Username }");
            sb.AppendLine($"Connected on             :{ session.BearerToken.IssuedOn }");
            sb.AppendLine($"Connection expires on    :{ session.BearerToken.ExpiresOn }");
            sb.AppendLine($"Connection expires in ms :{ session.BearerToken.ExpiresInMilliseconds }");
            sb.AppendLine($"Token type               :{ session.BearerToken.TokenType }");
            sb.Append($"Access token             :{ session.BearerToken.AccessToken }");
            Console.WriteLine(sb);
        }

        public void GetProductList()
        {
            var requestList = new Client.RequestProduct(session).List();
            requestList.Wait();
            var productList = requestList.Result;

            Console.ForegroundColor = CCCommands;
            Console.WriteLine($"# \tCategory\t\tDescription\t\t\t\t\tPrice");
            Console.ForegroundColor = CCInfo;
            foreach (var product in productList.Products.OrderByDescending(o => o.ProductNumber).OrderBy(o => o.CategoryName))
            {
                Console.WriteLine($"{product.ProductNumber}\t{product.CategoryName.PadRight(20, ' ')}\t{product.Name.PadRight(40, ' ')}\t{product.FaceValuePrice}");
            }
        }

        public void GetProduct(string productNumber)
        {
            var request = new Client.RequestProduct(session).Get(productNumber);
            request.Wait();
            var product = request.Result;

            if (product == null)
            {
                Console.ForegroundColor = CCWarning;
                Console.WriteLine($"Product not found with product number: {productNumber}");
                return;
            }
            Console.ForegroundColor = CCInfo;

            var sb = new StringBuilder();
            sb.AppendLine($"{Environment.NewLine}Product number: {product.ProductNumber}");
            sb.AppendLine($"Name: {product.Name}");
            sb.AppendLine($"Brand: {product.Brand}");
            sb.AppendLine($"Image URL: {product.ImageURL}");
            sb.AppendLine($"Category: {product.CategoryName}");
            sb.AppendLine($"Category Id: {product.CategoryId}");
            sb.AppendLine($"Face value: {product.FaceValuePrice}");
            sb.AppendLine($"Purchase price: {product.PurchasePrice}");
            sb.AppendLine($"Purchase vat: {product.PurchaseVat}");
            sb.AppendLine($"Max quantity per order: {product.MaxQuantityPerOrder}");
            sb.AppendLine($"Product type: {product.ProductType}");
            sb.Append($"Non recurring cost: {product.NonRecurringCost}");

            Console.WriteLine(sb);
        }

        public void BuyVoucher(string productNumber, string quantity, string subscription = "")
        {
            var request = new Client.RequestOrder(session).BuyVoucher(new Client.DTO.BuyVoucherRequest
            {
                ProductNumber = productNumber,
                Quantity = int.Parse(quantity),
                Subscription = subscription,
                DeliveryType = 0
            });

            request.Wait();

            var buyVoucherResponse = request.Result;

            if (buyVoucherResponse == null)
            {
                Console.ForegroundColor = CCWarning;
                Console.WriteLine("Something went wrong!");
                Console.WriteLine("Check your current balance.");
                Console.WriteLine("Check the product number.");
                Console.WriteLine("Or contact your distributor for more information.");
            }
            else
            {
                Console.ForegroundColor = CCInfo;
                var sb = new StringBuilder();
                foreach (var voucher in buyVoucherResponse)
                {
                    sb.AppendLine($"Title           : {voucher.Title}");
                    sb.AppendLine($"Product number  : {voucher.ProductNumber}");
                    sb.AppendLine($"Serial number   : {voucher.SerialNumber}");
                    sb.AppendLine($"Face value      : {voucher.FaceValue}");

                    sb.AppendLine($"Reprint code    : {voucher.ReprintCode}");
                    sb.AppendLine($"Receipt date    : {voucher.ReceiptDate}");

                    sb.AppendLine($"Helped by name  : {voucher.HelptByName}");
                    sb.AppendLine($"Company name    : {voucher.CompanyName}");
                    sb.AppendLine($"Address line 1  : {voucher.CompanyAddressLine1}");
                    sb.AppendLine($"Address line 2  : {voucher.CompanyAddressLine2}");
                    sb.AppendLine($"Text            : ");
                    sb.Append(voucher.Text);
                }

                Console.WriteLine(sb);
            }
        }

        public void GetCurrentBalance()
        {
            var requestList = new Client.RequestFinance(session).GetCurrentBalance();
            requestList.Wait();
            var currentBalance = requestList.Result;

            if (currentBalance == null)
            {
                Console.ForegroundColor = CCWarning;
                Console.WriteLine($"Something went wrong!");
            }
            else
            {
                Console.ForegroundColor = CCInfo;
                var sb = new StringBuilder();
                sb.AppendLine($"Balance          : {currentBalance.Balance}");
                sb.Append($"Balance + credit : {currentBalance.BalancePlusCredit}");

                Console.WriteLine(sb);
            }
        }

        public void Exit()
        {
            session.SEVClient.Dispose();
            session = null;
        }
    }
}
