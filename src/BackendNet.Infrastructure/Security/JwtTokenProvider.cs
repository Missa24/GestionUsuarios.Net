using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendNet.Application.Auth.Login;
using BackendNet.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackendNet.Infrastructure.Security;

public class JwtTokenProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;

    public JwtTokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(User user)
    {
        var key = _configuration["Jwt:Key"]!;

        var issuer = _configuration["Jwt:Issuer"];

        var audience = _configuration["Jwt:Audience"];

        var expiration =
            DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])
            );

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            );

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

        var token =
            new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}