using fosku_server.Models;

namespace fosku_server.Services.Auth;

public interface IAuthService
{
    public string GenerateToken(Person person);
    public string ValidateToken(string token);
    public Person? AuthenticatePerson(string password, Person person);
}