using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Seeders;

namespace SantaWishlist.Data;

public class SantaDbContext : IdentityDbContext<SantaUser>
{
    public DbSet<SantaUser> SantaUsers { get; set; }
    public DbSet<ChildSantaUser> ChildSantaUsers { get; set; }

    public SantaDbContext()
    {
    }

    public SantaDbContext(DbContextOptions<SantaDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.InitializeSeedData();
    }
}