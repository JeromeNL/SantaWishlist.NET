using Microsoft.AspNetCore.Identity;
using SantasWishlist.Domain;
using SantaWishlist.Data.DAL.Interfaces;
using SantaWishlist.Data.Models;

namespace SantaWishlist.Data.DAL;

public class ChildRepository : IChildRepository
{
    private readonly IGiftRepository _giftRepo;
    private readonly UserManager<SantaUser> _userManager;

    public ChildRepository(UserManager<SantaUser> userManager, IGiftRepository giftRepo)
    {
        _userManager = userManager;
        _giftRepo = giftRepo;
    }

    public async Task<Dictionary<GiftCategory, List<Gift>>> GetPresentsSortedByCategory()
    {
        var allGifts = _giftRepo.GetPossibleGifts();

        var sortedByCategory = allGifts
            .GroupBy(x => x.Category, x => x)
            .ToDictionary(x => x.Key, x => x.ToList());

        await Task.CompletedTask;
        return sortedByCategory;
    }

    public async Task LockUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTime.Now.AddYears(100);
        await _userManager.UpdateAsync(user);
    }

    public async Task SendWishlistToSanta(string username, List<Gift> gifts)
    {
        var wishList = new WishList
        {
            Name = username,
            Wanted = gifts
        };
        _giftRepo.SendWishList(wishList);
    }
}