using Application.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace Application
{
    public class TokenService : ITokenService
    {
       
        public string Generate(Guid userId, string role)
        {
            var claims = new[]
            {
                new Claim("id", userId.ToString()),
                new Claim(ClaimTypes.Role, role )

          };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER_SECRET_KEY_FOR_THIS_SUPER_PROJECT_YOU_KNOW_123"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
