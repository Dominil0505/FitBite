using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace ServerLibrary.Helpers
{
    public class JWThelper
    {
        private readonly string _secretKey;
        private readonly IConfiguration _configuration;

        public JWThelper(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _secretKey = _configuration.GetValue<string>("JwtSection:Key");
        }

        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration.GetValue<string>("JwtSection:Issuer"),
                ValidateAudience = true,
                ValidAudience = _configuration.GetValue<string>("JwtSection:Audience"),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (!(validatedToken is JwtSecurityToken jwtToken) ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userId;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
