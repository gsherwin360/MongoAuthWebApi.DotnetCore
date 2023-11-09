using MediatR;
using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.DTOs;
using MongoAuthWebApi.MongoDb.Identity;

namespace MongoAuthWebApi.Queries;

public record GetAllUsersQuery() : IRequest<List<UserDTO>>;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
{
    private readonly UserManager<MongoUser> _userManager;

    public GetAllUsersQueryHandler(UserManager<MongoUser> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users;
        var usersDTO = UserDTO.ToMongoUserDTOMapList(users);
        return usersDTO.ToList();
    }
}