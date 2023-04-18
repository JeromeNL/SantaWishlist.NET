using System.ComponentModel.DataAnnotations;
using SantaWishlist.Data;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Areas.Child.Models.Validations.Interfaces;

namespace SantaWishlistApp.Areas.Child.Models.Validations;

// ReSharper disable once ClassNeverInstantiated.Global
public class MaxPresentsPerCategory : IWishlistValidator
{
    public bool IsInvalid(GiftChooseViewModel model, ValidationContext context)
    {
        // Get logged in user
        var httpContextAccessor = (IHttpContextAccessor)context.GetService(typeof(IHttpContextAccessor))!;
        var dbContext = (SantaDbContext)context.GetService(typeof(SantaDbContext))!;
        var loggedUser = httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var user = dbContext.ChildSantaUsers.FirstOrDefault(x => x.UserName == loggedUser);

        // Rule 4
        if (this.ValidateWith<StijnExtraPresent>(model, context, user)) 
            return false;

        // Rule 5
        if (this.ValidateWith<VolunteerWork>(model, context, user)) 
            return false;

        // Rule 1
        if (this.ValidateWith<BehavedRule>(model, context, user))
            // Other rules allow extra presents, this one only allows specific amount
            return true;

        return model.SelectedGifts.Any(x => x.Value.Count > 3);
    }

    private bool ValidateWith<T>(GiftChooseViewModel model, ValidationContext context, ChildSantaUser user)
        where T : IMaxPresentValidator
    {
        return Activator.CreateInstance<T>().IsInvalid(model, context, user);
    }

}