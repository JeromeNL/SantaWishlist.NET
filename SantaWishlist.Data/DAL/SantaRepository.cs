using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SantasWishlist.Domain;
using SantaWishlist.Data.DAL.Interfaces;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;

namespace SantaWishlist.Data.DAL;

public class SantaRepository : ISantaRepository
{
    private readonly SantaDbContext _context;
    private readonly UserManager<SantaUser> _userManager;
    private readonly IGiftRepository _giftRepository;

    public SantaRepository(SantaDbContext context, UserManager<SantaUser> userManager, IGiftRepository IGiftRepository)
    {
        _context = context;
        _userManager = userManager;
        _giftRepository = IGiftRepository;
    }

    public async Task AddNewChildUser(ChildSantaUser user, Role role)
    {
        await _context.ChildSantaUsers.AddAsync(user);
        await _context.SaveChangesAsync();
        await _userManager.AddToRoleAsync(user, nameof(Role.Child));
        await _userManager.UpdateAsync(user);
    }

    public async Task AddRoleToUser(ChildSantaUser childUser, Role role)
    {
        await _userManager.AddToRoleAsync(childUser, nameof(Role.Child));
        await _userManager.UpdateAsync(childUser);
    }

    public async Task<SantaUser> GetSantaUser(HttpContext context)
    {
        var user = await _userManager.GetUserAsync(context.User);
        return user;
    }

    public List<Gift> GetPossibleGifts()
    {
        return _giftRepository.GetPossibleGifts();
    }

    public void SendWishlist(WishList wishList)
    {
        _giftRepository.SendWishList(wishList);
    }

    public async Task EnableAccountLock(SantaUser santaUser)
    {
        santaUser.LockoutEnabled = true;
        santaUser.LockoutEnd = DateTimeOffset.MaxValue;
        await _userManager.UpdateAsync(santaUser);
    }

    public List<String> GetDuplicatedNames(String names)
    {
        var tokens = names.Split(',').ToList();

        var duplicatesNames = tokens.GroupBy(x => x)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();
        return duplicatesNames;
    }

    public async Task<ChildSantaUser>? CreateNewChildUser(String name, bool wellBehaved, string password)
    {
        var childUser = new ChildSantaUser
        {
            UserName = name,
            NormalizedUserName = name.ToUpper(),
            WellBehaved = wellBehaved
        };

        var ph = new PasswordHasher<SantaUser>();
        childUser.PasswordHash = ph.HashPassword(childUser, password);
        return childUser;
    }

    public async Task<SantaUser> GetExistingUser(ChildSantaUser childUser)
    {
        return await _userManager.FindByNameAsync(childUser.UserName);
    }
}