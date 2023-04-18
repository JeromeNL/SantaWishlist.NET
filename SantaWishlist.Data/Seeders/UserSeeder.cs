using Microsoft.AspNetCore.Identity;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Seeders.Interfaces;

namespace SantaWishlist.Data.Seeders;

public class UserSeeder : ISeeder<SantaUser>
{
    public List<SantaUser> GetSeeds()
    {
        // Create santa user
        var appUser = new SantaUser
        {
            Id = DataSeeder.Santa_ID,
            Email = "test123@gmail.com",
            UserName = "Kerstman",
            NormalizedUserName = "KERSTMAN"
        };

        // Create children user
        var childrenUser = new SantaUser
        {
            Id = DataSeeder.Children_ID,
            Email = "child@gmail.com",
            UserName = "Kind",
            NormalizedUserName = "KIND"
        };

        // Set user password
        var ph = new PasswordHasher<SantaUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "kerstman");
        childrenUser.PasswordHash = ph.HashPassword(childrenUser, "kind");

        return new List<SantaUser>
        {
            appUser, childrenUser
        };
    }
}