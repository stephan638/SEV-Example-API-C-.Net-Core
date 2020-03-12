using Newtonsoft.Json;

namespace SEV.Client.DTO
{
    public partial class ProductProperty
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
    }
}
