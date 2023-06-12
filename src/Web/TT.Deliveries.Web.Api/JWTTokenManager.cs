using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TT.Deliveries.Data.Models;

namespace TT.Deliveries.Web.Api.Authentication
{
    public static class JwtTokenManager
    {
        private static readonly string SecretKey = "JwtSecretKey";
        private static readonly string Issuer = "JwtIssuer";
        private static readonly string Audience = "JwtAudience";

        public static string GenerateToken(Partner partner)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, partner.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Partner") // Assuming partner has a role property
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static Guid? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var claims = (validatedToken as JwtSecurityToken)?.Claims;
                if (claims != null)
                {
                    foreach (var claim in claims)
                    {
                        if (claim.Type == ClaimTypes.Name)
                        {
                            return Guid.Parse(claim.Value);
                        }
                    }
                }
            }
            catch (SecurityTokenException)
            {
                // Token validation failed
            }
            catch (Exception)
            {
                // Other exception occurred
            }

            return null;
        }
    }
}
