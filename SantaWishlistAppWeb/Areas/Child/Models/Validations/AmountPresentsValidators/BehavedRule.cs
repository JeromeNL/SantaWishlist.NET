using System.ComponentModel.DataAnnotations;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

public class BehavedRule : IMaxPresentValidator
{
    public Boolean IsInvalid(GiftChooseViewModel model, ValidationContext context, ChildSantaUser user)
    {
        // According to santa
        if (user.WellBehaved)
            return false;

        // If child lies
        if (model.ChildFormViewModel.Behaved == BehavedEnum.IkHebHetGeprobeerd)
            return model.SelectedGifts.Any(x => x.Value.Count > 1);

        // One present total
        var count = model.SelectedGifts.ToGiftList().Count(x => x.IsChecked);
        return count > 1;
    }
}