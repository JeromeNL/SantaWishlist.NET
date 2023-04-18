using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SantasWishlist.Domain;
using SantaWishlist.Data;
using SantaWishlist.Data.DAL;
using SantaWishlist.Data.DAL.Interfaces;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SantaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SantaWishlistConn")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultIdentity<SantaUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SantaDbContext>();


builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromDays(2);
    o.SlidingExpiration = true;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
    o.TokenLifespan = TimeSpan.FromHours(3));


builder.Services.AddSingleton<IGiftRepository, GiftRepository>();

builder.Services.AddScoped<IChildRepository, ChildRepository>();
builder.Services.AddScoped<ISantaRepository, SantaRepository>();


builder.Services.AddMvc();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Santa",
        policy => policy.RequireRole("Santa"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Children",
        policy => policy.RequireRole("Children"));
});

builder.Services.AddControllersWithViews();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseMigrationsEndPoint();
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


var childRole = nameof(Role.Child);
app.MapAreaControllerRoute(
    childRole,
    childRole,
    childRole + "/{controller=Home}/{action=Index}/{id?}");

var santaRole = nameof(Role.Santa);
app.MapAreaControllerRoute(
    santaRole,
    santaRole,
    santaRole + "/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();

public abstract partial class Program
{
}