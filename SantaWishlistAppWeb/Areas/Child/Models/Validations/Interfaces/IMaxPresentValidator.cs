using System.ComponentModel.DataAnnotations;
using SantaWishlist.Data.Models;

namespace SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;

public interface IMaxPresentValidator
{
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context, ChildSantaUser user);

}