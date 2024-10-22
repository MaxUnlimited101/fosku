using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fosku_server.Models;
using fosku_server.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace fosku_server.Services.Auth;

public class AuthService : IAuthService
{
    public string GenerateToken(Person person)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AuthSettings.JWTPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("userId", person.Id.ToString())
            ]
            ),
            Expires = DateTime.UtcNow.AddDays(15),
            SigningCredentials = credentials
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public string ValidateToken(string token)
    {
        token = token.Replace("Bearer ", "");
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var claims = jwtToken.Claims;
        var UserIdString = claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;

        return UserIdString ?? string.Empty;
    }

    public Person? AuthenticatePerson(string password, Person person)
    {
        string EnteredPasswordHash = HashingHelper.HashPassword(password, Convert.FromBase64String(person.SaltString));
        if (EnteredPasswordHash == person.PasswordHash)
        {
            return person;
        }
        return null;
    }

}