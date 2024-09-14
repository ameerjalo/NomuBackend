using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
namespace NomuBackend.Model
{

    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
    }

}
