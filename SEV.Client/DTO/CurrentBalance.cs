using Newtonsoft.Json;

namespace SEV.Client.DTO
{
    public partial class CurrentBalance
    {
        [JsonProperty(PropertyName = "Balance")]
        public decimal Balance { get; set; }

        [JsonProperty(PropertyName = "BalancePlusCredit")]
        public decimal BalancePlusCredit { get; set; }
    }
}
