using System.Collections.Generic;
using System.Linq;
using EasyShopping.Api.Constants;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Thinktecture.IdentityModel.Tokens;

namespace EasyShopping.Api.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudiencePropertyKey = "audience";

        private readonly string _issuer = string.Empty;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("data");
            }

            //string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ? data.Properties.Dictionary[AudiencePropertyKey] : null;
            string audienceId = ticket.Properties.Dictionary["userName"];

            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");
            
            string symmetricKeyAsBase64 = Const.Secret;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = ticket.Properties.IssuedUtc;
            var expires = ticket.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, ticket.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadToken(protectedText) as JwtSecurityToken;
            var identity = CreateUserIdentity(securityToken.Claims);
            var properties = CreateProperties(securityToken.Audiences.First());
            var ticket = new AuthenticationTicket(identity, properties);

            return ticket;
        }

        private static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                //{ "avatar", userName },
                //{ "roles", roleArr },
            };
            return new AuthenticationProperties(data);
        }

        private ClaimsIdentity CreateUserIdentity(IEnumerable<Claim> claims)
        {
            return new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        }
    }
}