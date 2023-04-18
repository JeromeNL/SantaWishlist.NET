using System.ComponentModel.DataAnnotations;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class SelectedInstrument : IWishlistValidator
{
    private const string _musicInstrument = "muziekinstrument";
    private const string _earplugs = "oordopjes";


    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        var gifts = model.SelectedGifts.ToGiftList();
        var hasNightLight = gifts.Any(g => g.Gift.Name.ToLower().Contains(_musicInstrument));
        var hasUnderWear = gifts.Any(g => g.Gift.Name.ToLower().Contains(_earplugs));
        return hasNightLight && !hasUnderWear;
    }
}