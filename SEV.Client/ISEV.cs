using SEV.Client.Config;

namespace SEV.Client
{
    internal interface ISEV
    {
        public SessionConfig Session { get; }
        public string Url { get; }
    }
}
