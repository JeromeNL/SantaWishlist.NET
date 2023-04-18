using System.ComponentModel.DataAnnotations;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

public class VolunteerWork : IMaxPresentValidator
{
    private const string _keyword = "vrijwilligerswerk";
    
    public Boolean IsInvalid(GiftChooseViewModel model, ValidationContext context, ChildSantaUser user)
    {
        return model.ChildFormViewModel.Explanation.ToLower().Contains(_keyword) &&
                 model.ChildFormViewModel.Behaved == BehavedEnum.HeelBraaf;
    }
}