using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;



namespace NomuBackend.Model
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {

    }
}
