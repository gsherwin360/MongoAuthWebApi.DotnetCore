using MongoAuthWebApi.MongoDb.Authentication;
using MongoAuthWebApi.MongoDb.Identity;

namespace MongoAuthWebApi.DTOs;

public class LoginDTO
{
    public string Token { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public DateTime LastActivityOn { get; set; }

    public LoginDTO(AuthenticationResult result)
    {
        if (result.User is not MongoUser user)
        {
            throw new InvalidOperationException("Cannot cast into MongoUser");
        }

        this.Token = result.Token;
        this.FirstName = user.FirstName;
        this.Surname = user.LastName;
        this.Email = user.Email;
        this.LastActivityOn = user.LastActivityOn;
    }

    public LoginDTO()
    {
    }
}
