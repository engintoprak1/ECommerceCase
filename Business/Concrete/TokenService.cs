using Business.Abstract;
using Domain.Concrete.Dtos.Auth;
using Domain.Concrete.Entities.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Business.Concrete;

public class TokenService(IConfiguration configuration) : ITokenService
{

    public Task<TokenResponse> CreateToken(ApplicationUser user)
    {
        string secretKey = configuration["Jwt:Secret"]!;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("email_verified", user.EmailConfirmed.ToString()),
            ]),
            Expires = DateTime.Now.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        JsonWebTokenHandler handler = new();

        string token = handler.CreateToken(tokenDescriptor);

        TokenResponse response = new TokenResponse()
        {
            access_token = token,
            expires_in = tokenDescriptor.Expires,
            token_type = "Bearer",
        };

        return Task.FromResult(response);
    }
}
