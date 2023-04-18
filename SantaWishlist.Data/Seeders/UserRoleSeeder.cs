using Microsoft.AspNetCore.Identity;
using SantaWishlist.Data.Seeders.Interfaces;

namespace SantaWishlist.Data.Seeders;

public class UserRoleSeeder : ISeeder<IdentityUserRole<string>>
{
    public List<IdentityUserRole<string>> GetSeeds()
    {
        // Set user role to admin
        var santaUserRole = new IdentityUserRole<string>
        {
            RoleId = DataSeeder.SantaRole_ID,
            UserId = DataSeeder.Santa_ID
        };

        var childUserRole = new IdentityUserRole<string>
        {
            RoleId = DataSeeder.ChildrenRole_ID,
            UserId = DataSeeder.Children_ID
        };

        return new List<IdentityUserRole<string>>
        {
            childUserRole, santaUserRole
        };
    }
}