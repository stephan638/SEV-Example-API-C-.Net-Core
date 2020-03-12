using SEV.Client.Config;

namespace SEV.Client.DTO
{
    public class RequestBase : ISEV
    {
        public SessionConfig Session { get; private set; }
        public string Url { get; set; }

        public RequestBase(SessionConfig session)
        {
            Session = session;
        }
    }
}
