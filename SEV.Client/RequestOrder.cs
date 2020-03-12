using Newtonsoft.Json;
using SEV.Client.Config;
using SEV.Client.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SEV.Client
{
    public class RequestOrder : RequestBase
    {
        public RequestOrder(SessionConfig session) : base(session)
        {
        }

        public async Task<List<BuyVoucherResponse>> BuyVoucher(BuyVoucherRequest buyVoucher)
        {
            Url = Session.BaseUrl + BasePath.BuyVoucher;
            StringContent formBody = new StringContent(JsonConvert.SerializeObject(buyVoucher), Encoding.UTF8, "application/json");

            var response = await Session.SEVClient.PostAsync(Url, formBody);

            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BuyVoucherResponse>>(str); ;
            }

            return null;
        }
    }
}
