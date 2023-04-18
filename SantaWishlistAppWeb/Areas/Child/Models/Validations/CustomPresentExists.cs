using System.ComponentModel.DataAnnotations;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class CustomPresentExists : IWishlistValidator
{
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        foreach (var customGift in model.OtherWishesList)
            return model.CategoryGiftsDict.ToGiftList()
                .Select(x => x.Gift.Name.ToLower())
                .Contains(customGift.ToLower());
        return false;
    }
}