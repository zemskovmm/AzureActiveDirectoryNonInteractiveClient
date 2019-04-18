This is a Azure Active Directory demo app with non-interactive .net client app and server with action that requires authentication. Authentication is done with AAD.

###We have 3 high level components involved 

 - .net client (Console app)
 - .net server (ASP.net)
 - Azure infrastructure (As authentication authority)

###.net client highlits: 

- No user interaction involved.
- Requires Identity(Client) and its secret (Those reside in app config file)

###Server app highlights:

- Has bare minimum code related to authentication process.


Step-by-step instruction:

 1. Create Azure App registration.
 - Type of Application: WebApp / API
 - Homepage: Unique url (I have used https://localhost:44338/). This address is automatically added to reply URLs in App Registration settings.

 2. Create server application.
 - Create new project in Visual Studio (ASP.NET Web Application, Web API with no authentication)
 - Configure authentication:
    - Install Owin ActiveDirectory nuget package (Microsoft.Owin.Security.ActiveDirectory)
    - Add Tenant, Audience, ClientID to configuration. (Get those from AppRegistration)
    - Install Microsoft.Owin.Host.SystemWeb nuget package (It enables you to configure owin with Starup.cs). More info: https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-startup-class-detection
    - Configure authentication. Create Startup.cs file and fill with this:

            using Microsoft.IdentityModel.Tokens;
            using Microsoft.Owin.Security.ActiveDirectory;
            using Owin;
            using System.Configuration;

            namespace SecureWebApp
            {
                public partial class Startup
                {
                    public void Configuration(IAppBuilder app)
                    {
                        app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                        new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                        {
                            Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
                            TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
                            },
                        });
                    }
                }
            }

 3. Create non-interactive client. We are going to authenticate through Bearer token.
 - Add to configuratin file ClientId, ClientSecret, AuthString, Resource. All those values you get from your app registration.
 - Add Microsoft.IdentityModel.Clients.ActiveDirectory nuget package
 - Get the token:
    I have created very simple method to extact token:
            var authString = ConfigurationManager.AppSettings["AuthString"];
            var clientId = ConfigurationManager.AppSettings["ClientId"];
            var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            var resource = ConfigurationManager.AppSettings["Resource"];
            var authContext = new AuthenticationContext(authString);
            var credentials = new ClientCredential(clientId, clientSecret);
            var tokenResult = authContext.AcquireTokenAsync(resource, credentials).Result;
            return tokenResult.AccessToken;
 - Before making a http call add to httpClient default authentication header, to authenticate all calls:
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

You are all set!
