using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace MongoAuthWebApi.MongoDb.Identity;

[CollectionName("users")]
public class MongoUser : MongoIdentityUser<Guid>
{
    public override string UserName { get => Email; set => Email = value; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; }

    public DateTime LastModifiedDate { get; set; }
}
