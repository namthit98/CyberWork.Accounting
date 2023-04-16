using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CyberWork.Accounting.Infrastructure.Identity;

public class TokenServices : ITokenServices
{
    private readonly IConfiguration _configuration;
    public TokenServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(IAppUser user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}