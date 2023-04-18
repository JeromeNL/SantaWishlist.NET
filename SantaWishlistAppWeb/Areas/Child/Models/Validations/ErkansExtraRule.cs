using System.ComponentModel.DataAnnotations;
using SantasWishlist.Domain;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class ErkansExtraRule : IWishlistValidator
{
    private const string _nightlight = "nachtlampje";
    
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        var gifts = model.SelectedGifts.ToGiftList();
        var hasGamingItem = gifts.Any(g => g.Gift.Category == GiftCategory.READ);
        var tutoring = gifts.Any(g => g.Gift.Name.ToLower().Contains(_nightlight));
        return hasGamingItem && !tutoring;
    }
}