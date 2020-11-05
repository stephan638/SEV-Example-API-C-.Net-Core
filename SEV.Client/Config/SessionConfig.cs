using SEV.Client.DTO;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SEV.Client.Config
{
    public class SessionConfig
    {
        public Uri BaseUrl = new Uri("https://sev-api.allservices.nl/");
        public HttpClient SEVClient { get; private set; }
        public BearerToken BearerToken { get; private set; }

        public bool LoginSuccessful { get; private set; }

        public async static Task<SessionConfig> GetSessionConfigAsync(string userName, string password) 
        {
            var session = new SessionConfig();
            session.BearerToken = await RequestLogin.Login(session.BaseUrl, userName, password);

            if (session.BearerToken == null)
            {
                session.LoginSuccessful = false;
                return session;
            }
            else
            {
                session.LoginSuccessful = true;
            }

            session.SEVClient = new HttpClient();
            session.SEVClient.BaseAddress = session.BaseUrl;
            session.SEVClient.DefaultRequestHeaders.Accept.Clear();
            session.SEVClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            session.SEVClient.DefaultRequestHeaders.Add("authorization", $"{session.BearerToken.TokenType} {session.BearerToken.AccessToken}");

            return session;
        }
    }
}
