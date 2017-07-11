using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
//using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using EasyShopping.Api.Providers;
using EasyShopping.Api.Models;
using EasyShopping.Api.Constants;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using System.Threading.Tasks;

namespace EasyShopping.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"), // POST to /Token will call ApplicationOAuthProvider
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                RefreshTokenProvider = new ApplicationRefreshTokenProvider(),
                AccessTokenFormat = new CustomJwtFormat(Const.Issuer),
                AccessTokenExpireTimeSpan = Const.TokenTimeSpan,

                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true,
            };

            // Enable the application to use bearer tokens to authenticate users
            //app.UseOAuthBearerTokens(OAuthOptions);

            app.UseOAuthAuthorizationServer(OAuthOptions);
            //System.Diagnostics.Debugger.Launch();
            app.UseJwtBearerAuthentication(
               new JwtBearerAuthenticationOptions
               {               
                   AuthenticationMode = AuthenticationMode.Active,
                   AllowedAudiences = new[] { "admin", "chushop1", "chushopvanhanvien1" },
                   IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                   {
                        new SymmetricKeyIssuerSecurityTokenProvider(Const.Issuer, Const.Secret)
                   },
                   Provider = new OAuthBearerAuthenticationProvider
                   {
                       OnValidateIdentity = context =>
                       {
                           // Add new claim here
                           // context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("newCustomClaim", "newValue"));
                           return Task.FromResult<object>(null);
                       }
                   }
               });

            // Uncomment the following lines to enable logging in with third party login providers

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
