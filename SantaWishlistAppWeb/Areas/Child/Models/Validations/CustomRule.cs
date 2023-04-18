using System.ComponentModel.DataAnnotations;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class CustomRule : IWishlistValidator
{
    private const string _computerGame = "computerspel";
    private const string _computer = "spelcomputer";
    private const string _extraClasses = "bijles";
    
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        var gifts = model.SelectedGifts.ToGiftList();
        var hasGamingItem = gifts.Any(g => g.Gift.Name.ToLower().Contains(_computerGame)) ||
                            gifts.Any(g => g.Gift.Name.ToLower().Contains(_computer));
        var tutoring = gifts.Any(g => g.Gift.Name.ToLower().Contains(_extraClasses));
        return hasGamingItem && !tutoring;
    }
}