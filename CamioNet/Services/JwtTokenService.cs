using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace camionet.Services
{
    public class JwtTokenService
    {

        private readonly string _secretKey;
        private readonly string _issuer;
        //private readonly JwtSettings _settings;
        private readonly string _audience;
        private readonly TimeSpan _accessTokenExpiration;

        public JwtTokenService(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("SecretKey");
            _issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("Issuer");
            _audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("Audience");
            _accessTokenExpiration = TimeSpan.TryParse(configuration["JwtSettings:AccessTokenExpiration"], out var expiration)
                ? expiration : throw new ArgumentException("Invalid AccessTokenExpiration format");

        }

        // Genera un token de acceso (JWT)
        public string GenerateAccessToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.UtcNow.Add(_accessTokenExpiration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
