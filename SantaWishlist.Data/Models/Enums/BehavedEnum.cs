using System.ComponentModel.DataAnnotations;

namespace SantaWishlist.Data.Models.Enums;

public enum BehavedEnum
{
    [Display(Name="Heel braaf")]
    HeelBraaf,

    [Display(Name="Een Beetje Braaf")]
    EenBeetjeBraaf,

    [Display(Name="Ik Heb Het Geprobeerd")]
    IkHebHetGeprobeerd
}

public static class BehavedEnumExtensions
{
    public static string GetDisplayName(this BehavedEnum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
        return attributes.Length > 0 ? attributes[0].Name : value.ToString();
    }
}