using System.ComponentModel.DataAnnotations;

namespace SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;

public interface IWishlistValidator
{
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context);
}