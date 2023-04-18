using System.ComponentModel.DataAnnotations;
using SantasWishlist.Domain;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

public class StijnExtraPresent : IMaxPresentValidator
{
    private const string _userNameForExtraPresent = "stijn";
    private const string _bookName = "dolfje weerwolfje";

    
    public Boolean IsInvalid(GiftChooseViewModel model, ValidationContext context, ChildSantaUser user)
    {

        if (!user.UserName.ToLower().Equals(_userNameForExtraPresent)) return false;
        
        var gifts = model.SelectedGifts.ToGiftList();
        var hasDolfje = gifts.Any(g => g.Gift.Name.ToLower().Contains(_bookName));
        if (!hasDolfje) return false;
        
        var allMax3 = model.SelectedGifts.Where(e => e.Key != GiftCategory.READ).Any(x => x.Value.Count > 3);
        var booksMax4 = model.SelectedGifts.Where(e => e.Key == GiftCategory.READ).Any(x => x.Value.Count > 4);
        var total = allMax3 && booksMax4;
        return total;

    }
}