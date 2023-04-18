using System.ComponentModel.DataAnnotations;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class NightLight : IWishlistValidator
{
    
    private const string _nightLight = "nachtlampje";
    private const string _underwear = "ondergoed";
    
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        var gifts = model.SelectedGifts.ToGiftList();
        var hasNightLight = gifts.Any(g => g.Gift.Name.ToLower().Contains(_nightLight));
        var hasUnderWear = gifts.Any(g => g.Gift.Name.ToLower().Contains(_underwear));
        if (hasNightLight && !hasUnderWear)
        {
            return true;
        }

        return false;
    }
}