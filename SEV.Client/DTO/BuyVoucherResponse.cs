using Newtonsoft.Json;
using System;

namespace SEV.Client.DTO
{
    public partial class BuyVoucherResponse
    {
        [JsonProperty(PropertyName = "ProductNumber")]
        public string ProductNumber { get; set; }

        [JsonProperty(PropertyName = "SerialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty(PropertyName = "ActivationCode")]
        public string ActivationCode { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "FaceValue")]
        public decimal FaceValue { get; set; }

        [JsonProperty(PropertyName = "ReprintCode")]
        public string ReprintCode { get; set; }

        [JsonProperty(PropertyName = "ReceiptDate")]
        public DateTime ReceiptDate { get; set; }

        [JsonProperty(PropertyName = "HelptByName")]
        public string HelptByName { get; set; }

        [JsonProperty(PropertyName = "CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "CompanyAddressLine1")]
        public string CompanyAddressLine1 { get; set; }

        [JsonProperty(PropertyName = "CompanyAddressLine2")]
        public string CompanyAddressLine2 { get; set; }
    }
}
