using System.ComponentModel.DataAnnotations;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class LegoAndKnex : IWishlistValidator
{
    private const string legoName = "lego";
    private const string knexName = "k`nex";

    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        var gifts = model.SelectedGifts.ToGiftList();
        var hasKnex = gifts.Any(g => g.Gift.Name.ToLower().Contains(knexName));
        var hasLego = gifts.Any(g => g.Gift.Name.ToLower().Contains(legoName));
        return hasKnex && hasLego;
    }
}