using Newtonsoft.Json;
using System.Collections.Generic;

namespace SEV.Client.DTO
{
    public partial class ProductList
    {
        public ProductList()
        {
            this.Products = new HashSet<Product>();
        }

        [JsonProperty(PropertyName = "ProductList")]
        public HashSet<Product> Products { get; set; }
    }

    public partial class Product
    {
        public Product()
        {
            this.ProductProperties = new HashSet<ProductProperty>();
        }

        [JsonProperty(PropertyName = "ProductNumber")]
        public string ProductNumber { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Brand")]
        public string Brand { get; set; }

        [JsonProperty(PropertyName = "CategoryName")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "CategoryId")]
        public int CategoryId { get; set; }

        [JsonProperty(PropertyName = "FaceValuePrice")]
        public decimal FaceValuePrice { get; set; }

        [JsonProperty(PropertyName = "PurchasePrice")]
        public decimal PurchasePrice { get; set; }

        [JsonProperty(PropertyName = "PurchaseVat")]
        public decimal PurchaseVat { get; set; }

        [JsonProperty(PropertyName = "MaxQuantityPerOrder")]
        public int MaxQuantityPerOrder { get; set; }

        [JsonProperty(PropertyName = "ProductType")]
        public string ProductType { get; set; }

        [JsonProperty(PropertyName = "ImageURL")]
        public string ImageURL { get; set; }

        [JsonProperty(PropertyName = "NonRecurringCost")]
        public decimal NonRecurringCost { get; set; }

        [JsonProperty(PropertyName = "ProductProperties")]
        public HashSet<ProductProperty> ProductProperties { get; set; }
    }
}
