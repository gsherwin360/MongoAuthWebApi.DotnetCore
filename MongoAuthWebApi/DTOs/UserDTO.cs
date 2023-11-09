using MongoAuthWebApi.MongoDb.Identity;

namespace MongoAuthWebApi.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? LastActivityOn { get; set; }

    public static IEnumerable<UserDTO> ToMongoUserDTOMapList(IEnumerable<MongoUser> source)
    {
        if (source is null)
        {
            throw new InvalidOperationException("Cannot mapped into MongoUser");
        }

        return source.Select(item => new UserDTO
        {
            Id = item.Id,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Email = item.Email,
            CreatedOn = item.CreatedOn,
            ModifiedOn = item.ModifiedOn,
            LastActivityOn = item.LastActivityOn
        });
    }
}