using SEV.Client.DTO;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SEV.Client.Config
{
    public class SessionConfig
    {
        public Uri BaseUrl { get; private set; }
        public HttpClient SEVClient { get; private set; }
        public BearerToken BearerToken { get; private set; }

        public bool LoginSuccessful { get; private set; }

        public SessionConfig(string userName, string password)
        {
            BaseUrl = new Uri("https://sev-api.allservices.nl/");

            var logonRequest = new RequestLogin().Login(BaseUrl, userName, password);
            logonRequest.Wait();

            BearerToken = logonRequest.Result;

            if (BearerToken == null)
            {
                LoginSuccessful = false;
                return;
            }
            else
            {
                LoginSuccessful = true;
            }

            SEVClient = new HttpClient();
            SEVClient.BaseAddress = BaseUrl;
            SEVClient.DefaultRequestHeaders.Accept.Clear();
            SEVClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            SEVClient.DefaultRequestHeaders.Add("authorization", $"{BearerToken.TokenType} {BearerToken.AccessToken}");
        }
    }
}
