using Newtonsoft.Json;
using SEV.Client.Config;
using SEV.Client.DTO;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SEV.Client
{
    public class RequestLogin
    {
        public async Task<BearerToken> Login(Uri baseUrl, string userName, string password)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = baseUrl;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                StringContent formBody = new StringContent($"grant_type=password&username={userName}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await httpClient.PostAsync(BasePath.Token, formBody);
                httpClient.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    var str = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BearerToken>(str); ;
                }
            }

            return null;
        }
    }
}
