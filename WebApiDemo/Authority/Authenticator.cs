using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WebApiDemo.Authority
{
    public class Authenticator
    {
        public static bool Authenticate(string clientId, string secret)
        {
            var app = ApplicationRepository.GetApplicationByClientId(clientId);

            if (app == null)
            {
                return false;
            }
            return app.ClientId == clientId && app.Secret == secret;

        }
        public static string CreateToken(string clientId, DateTime expiresAt, string? strSecretKey)
        {
            var app = ApplicationRepository.GetApplicationByClientId(clientId);
            var claims = new List<Claim>() {
                new Claim("ApplicationName", app?.ApplicationName ?? string.Empty),
                new Claim("Read",(app?.Scopes??string.Empty).Contains("read")?"true":"false"),
                new Claim("Write",( app?.Scopes??string.Empty).Contains("read") ? "true" : "false")
                };
            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);
            var jwt = new JwtSecurityToken(
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public static bool VerifyToken(string token, string strSecretKey)
        {

            if(string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            if(token.StartsWith("Bearer"))
            {
              token = token.Substring(6).Trim();
            }

            var secretKey = Encoding.ASCII.GetBytes(strSecretKey);
            SecurityToken securityToken;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out securityToken);
            }
            catch(SecurityTokenException)
            {
                return false;
            }
            catch 
            {
                throw;
            }
            return securityToken != null;
        }

    }
}