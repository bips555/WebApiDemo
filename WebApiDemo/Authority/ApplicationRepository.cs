namespace WebApiDemo.Authority
{
    public static class ApplicationRepository
    {
               public static List<Application> _applications = new List<Application>()
            { 
                   new Application()
            {
                ApplicationId = 1,
                ApplicationName = "WebApp",
                ClientId="8A1F3D2B-91C9-4A37-91D1-5C857EE36F19",
                Secret="7C3A8DF9-5D7B-42F6-9E7E-4A89DB6F8C1B",
                Scopes ="read,write"
            }
            };
        public static bool Authenticate(string clientId,string secret)
        {
            return _applications.Any(a => a.ClientId == clientId && a.Secret == secret);
        }
        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x=>x.ClientId == clientId);
        }
        }
    }


