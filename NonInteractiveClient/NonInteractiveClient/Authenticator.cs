using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;

namespace NonInteractiveClient
{
    public class Authenticator
    {
        public string GetToken()
        {
            var authString = ConfigurationManager.AppSettings["AuthString"];
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            var resource = ConfigurationManager.AppSettings["Resource"];
            var authContext = new AuthenticationContext(authString);
            var credentials = new ClientCredential(clientId, clientSecret);
            var tokenResult = authContext.AcquireTokenAsync(resource, credentials).Result;
            return tokenResult.AccessToken;
        }
    }
}
