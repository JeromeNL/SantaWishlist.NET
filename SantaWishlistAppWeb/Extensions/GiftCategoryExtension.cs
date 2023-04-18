using SantasWishlist.Domain;
using SantaWishlist.Data.Models;

namespace SantaWishlistApp.Extensions;

public static class GiftCategoryExtension
{
    public static Dictionary<GiftCategory, List<GiftCheckboxModel>> ToCheckboxModel(this Dictionary<GiftCategory, List<Gift>> giftsByCategory)
    {
        // Create new dictionary
        var giftsWithCheckbox = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();

        // Enter keys
        giftsByCategory.Keys.ToList().ForEach(x => giftsWithCheckbox.Add(x, new List<GiftCheckboxModel>()));

        // Enter values
        foreach (var category in giftsByCategory.Keys)
            giftsByCategory[category].ForEach(x => giftsWithCheckbox[category].Add(new GiftCheckboxModel(x)));

        return giftsWithCheckbox;
    }

    public static List<GiftCheckboxModel> ToGiftList(this Dictionary<GiftCategory, List<GiftCheckboxModel>> giftsByCategory)
    {
        var gifts = new List<GiftCheckboxModel>();
        foreach (var kvp in giftsByCategory)
            gifts.AddRange(kvp.Value);
        return gifts;
    }
}