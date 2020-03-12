namespace SEV.Client.Config
{
    internal class BasePath
    {
        public const string Token = "token";

        public const string GetProductList = "product/getproductList";
        public const string GetProduct = "Product/getProduct?productnumber=";

        public const string AllInvoices = "finance/allInvoice";
        public const string GetInvoice = "finance/getInvoice";
        public const string GetInvoiceAsPdf = "finance/getInvoiceAsPdf";
        public const string Balance = "finance/getCurrentBalance";

        public const string BuyVoucher = "order/buyvouchers";
        public const string GetSlipText = "order/getsliptext";
    }
}
