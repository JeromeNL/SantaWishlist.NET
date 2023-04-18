using SantasWishlist.Domain;

namespace SantaWishlist.Data.DAL.Interfaces;

public interface IChildRepository
{
    Task<Dictionary<GiftCategory, List<Gift>>> GetPresentsSortedByCategory();
    Task LockUser(string userId);
    Task SendWishlistToSanta(string username, List<Gift> gifts);
}