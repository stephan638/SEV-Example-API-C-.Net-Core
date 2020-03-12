using SEV.Client.Config;
using SEV.Client.DTO;
using System.Threading.Tasks;

namespace SEV.Client
{
    public class RequestProduct : RequestBase
    {
        public RequestProduct(SessionConfig session) : base(session)
        {
        }

        public async Task<Product> Get(string productNumber)
        {
            Url = Session.BaseUrl + BasePath.GetProduct + productNumber;
            var response = await Session.SEVClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(json);
            }

            return null;
        }

        public async Task<ProductList> List()
        {
            Url = Session.BaseUrl + BasePath.GetProductList;
            var response = await Session.SEVClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ProductList>(str);
            }

            return null;
        }
    }
}
