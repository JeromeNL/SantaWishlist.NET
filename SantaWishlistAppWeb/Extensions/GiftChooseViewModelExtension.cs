using System.ComponentModel.DataAnnotations;
using SantasWishlist.Domain;
using SantaWishlistApp.Areas.Child.Models;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;

namespace SantaWishlistApp.Extensions;

public static class GiftChooseViewModelExtension
{
    public static List<Gift> ToWishlist(this GiftChooseViewModel viewModel)
    {
        var wishlist = new List<Gift>();
        var gifts = viewModel.CategoryGiftsDict.Select(x => x.Value).ToList();
        gifts.ForEach(x => x.ForEach(x => wishlist.Add(x.Gift)));
        return wishlist;
    }

    public static bool ValidateWith<T>(this GiftChooseViewModel viewModel, ValidationContext context) where T : IWishlistValidator
    {
        return Activator.CreateInstance<T>().IsInvalid(viewModel, context);
    }
}