using SantasWishlist.Domain;

namespace SantaWishlist.Data.Models;

public class GiftCheckboxModel
{
    public GiftCheckboxModel()
    {
    }

    public GiftCheckboxModel(Gift gift)
    {
        Gift = gift;
        IsChecked = false;
    }

    public Gift Gift { get; set; }
    public bool IsChecked { get; set; }
}