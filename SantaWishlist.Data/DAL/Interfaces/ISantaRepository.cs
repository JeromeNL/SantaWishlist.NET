using Microsoft.AspNetCore.Http;
using SantasWishlist.Domain;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;

namespace SantaWishlist.Data.DAL.Interfaces;

public interface ISantaRepository
{
    public Task AddNewChildUser(ChildSantaUser user, Role role);

    public Task AddRoleToUser(ChildSantaUser user, Role role);

    public Task<SantaUser> GetSantaUser(HttpContext context);

    public List<Gift> GetPossibleGifts();
    public void SendWishlist(WishList wishList);

    public Task EnableAccountLock(SantaUser santaUser);

    public List<String> GetDuplicatedNames(String names);
    public Task<ChildSantaUser> CreateNewChildUser(String name, bool wellBehaved, string password);

    public Task<SantaUser> GetExistingUser(ChildSantaUser childUser);
}