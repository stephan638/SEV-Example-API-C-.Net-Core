using Newtonsoft.Json;

namespace SEV.Client.DTO
{
    public partial class BuyVoucherRequest
    {
        [JsonProperty(PropertyName = "ProductNumber")]
        public string ProductNumber { get; set; }

        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "Subscription")]
        public string Subscription { get; set; }

        [JsonProperty(PropertyName = "DeliveryType")]
        public int DeliveryType { get; set; }
    }
}
