using SEV.Client.Config;
using SEV.Client.DTO;
using System.Threading.Tasks;

namespace SEV.Client
{
    public class RequestFinance : RequestBase
    {
        public RequestFinance(SessionConfig session) : base(session)
        {
        }

        public async Task<CurrentBalance> GetCurrentBalance()
        {
            Url = Session.BaseUrl + BasePath.Balance;
            var response = await Session.SEVClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentBalance>(str);
            }

            return null;
        }
    }
}
