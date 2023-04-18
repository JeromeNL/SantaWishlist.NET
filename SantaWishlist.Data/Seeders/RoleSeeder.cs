using Microsoft.AspNetCore.Identity;
using SantaWishlist.Data.Models.Enums;
using SantaWishlist.Data.Seeders.Interfaces;

namespace SantaWishlist.Data.Seeders;

public class RoleSeeder : ISeeder<IdentityRole<string>>
{
    public List<IdentityRole<string>> GetSeeds()
    {
        // Seed santa role
        var santaRole = new IdentityRole
        {
            Name = nameof(Role.Santa),
            NormalizedName = nameof(Role.Santa).ToUpper(),
            Id = DataSeeder.SantaRole_ID,
        };

        // Seed children role
        var childRole = new IdentityRole
        {
            Name = nameof(Role.Child),
            NormalizedName = nameof(Role.Child).ToUpper(),
            Id = DataSeeder.ChildrenRole_ID,
        };

        return new List<IdentityRole<string>>
        {
            childRole, santaRole
        };
    }
}