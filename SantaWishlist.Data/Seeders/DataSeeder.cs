using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SantaWishlist.Data.Models;

namespace SantaWishlist.Data.Seeders;

public static class DataSeeder
{
    public static string Santa_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
    public static string SantaRole_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

    public static string Children_ID = "02174cf0–9412–4cfe-afbf-59f706d72ce1";
    public static string ChildrenRole_ID = "341743f0-asd2–42de-afbf-59kmkkmk72ce1";

    public static void InitializeSeedData(this ModelBuilder model)
    {
        var roles = new RoleSeeder().GetSeeds();
        var users = new UserSeeder().GetSeeds();
        var userRoles = new UserRoleSeeder().GetSeeds();

        model.Entity<IdentityRole>().HasData(roles);
        model.Entity<SantaUser>().HasData(users);
        model.Entity<IdentityUserRole<string>>().HasData(userRoles);
    }
}