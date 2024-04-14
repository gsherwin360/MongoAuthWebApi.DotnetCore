using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.MongoDb.Identity;
using MongoAuthWebApi.Primitives;

namespace MongoAuthWebApi.Commands;

public record RegisterCommand(string Email, string Password) : IRequest<Result<Guid>>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<Guid>>
{
    private readonly UserManager<MongoUser> _userManager;

    public RegisterCommandHandler(UserManager<MongoUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (IsUserExists(request.Email))
        {
            return Result<Guid>.Failure(new Error("UserAlreadyExist", "User is already exist."));
        }

        var user = CreateUser(request);

        var resultCreate = await _userManager.CreateAsync(user, request.Password);

        if (resultCreate.Succeeded)
        {
            return Result<Guid>.Success(user.Id);
        }

        return Result<Guid>.Failure(new Error(resultCreate.Errors.First().Code, resultCreate.Errors.First().Description));
    }

    private bool IsUserExists(string email)
    {
        return _userManager.Users.Any(u => u.Email == email);
    }

    private MongoUser CreateUser(RegisterCommand request)
    {
        var user = new MongoUser
        {
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        return user;
    }
}
