using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System.Configuration;

namespace SecuredWebApp
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
