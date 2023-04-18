using System.ComponentModel.DataAnnotations;
using SantasWishlist.Domain;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class AgeRelatedGifts : IWishlistValidator
{
    private const int _amountOfExceptions = 1;

    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        var service = (IGiftRepository)context.GetService(typeof(IGiftRepository))!;

        var exceptions = _amountOfExceptions;
        foreach (var gift in model.SelectedGifts.ToGiftList())
        {
            // If the gift is meant for children same age or younger
            if (service.CheckAge(gift.Gift.Name) <= model.ChildFormViewModel.Age) continue;

            if (exceptions <= 0) return true;

            exceptions--;
        }

        return false;
    }
}