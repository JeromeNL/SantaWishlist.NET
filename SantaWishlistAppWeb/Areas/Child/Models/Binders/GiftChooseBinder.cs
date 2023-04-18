using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SantasWishlist.Domain;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Utils;

namespace SantaWishlistApp.Areas.Child.Models.Binders;

public class GiftChooseBinder : IModelBinder
{
    private const string _bracketsPattern = @"\[(.*?)\]";

    // Properties
    private readonly string _giftName = nameof(Gift) + "." + ReflectionHelper.PropertyName<Gift>(x => x.Name);
    private readonly string _isChecked = ReflectionHelper.PropertyName<GiftCheckboxModel>(m => m.IsChecked);

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));
        var newDict = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();

        // Get values
        var form = bindingContext.HttpContext.Request.Form;
        var subtractedKeys = form.Keys.Select(x => x.Replace(bindingContext.FieldName, "")).ToList();

        // Initialize map
        var categoryNames = subtractedKeys
            .Select(GetBetweenBrackets)
            .Where(x => x != null)
            .Distinct()
            .ToList();

        // Fill new dictionary
        foreach (var categoryName in categoryNames)
        {
            // Initialize list
            var dictKey = Enum.Parse<GiftCategory>(categoryName);
            newDict.Add(dictKey, new List<GiftCheckboxModel>());

            var categoryItems = subtractedKeys
                .Where(x => x.Contains(categoryName))
                .Select(x => x.Replace($"[{categoryName}]", ""))
                .ToList();

            // Group by item with properties
            var groupedStrings = categoryItems
                .GroupBy(GetBetweenBrackets)
                .Select(g => new Dictionary<string, List<string>>
                {
                    {
                        g.Key, g.ToList()
                            .Select(x => Regex.Replace(x, _bracketsPattern + ".", ""))
                            .ToList()
                    }
                })
                .ToList();


            // Generate new items
            foreach (var grouped in groupedStrings)
            {
                var id = grouped.Keys.First();
                var values = grouped.Values.First();

                if (!values.Any(x => x.Contains(_giftName))) continue;

                var keyPrefix = bindingContext.FieldName + $"[{categoryName}][{id}].";
                try
                {
                    var checkedKey = form[keyPrefix + _isChecked].First();
                    var gift = new GiftCheckboxModel
                    {
                        Gift = new Gift
                        {
                            Category = dictKey,
                            Name = values.Contains(_giftName) ? form[keyPrefix + _giftName].First() : ""
                        },
                        IsChecked = values.Contains(_isChecked) && bool.Parse(checkedKey)
                    };
                    newDict[dictKey].Add(gift);
                }
                catch (Exception)
                {
                }
            }
        }


        bindingContext.Result = ModelBindingResult.Success(newDict);
        await Task.CompletedTask;
    }

    private static string? GetBetweenBrackets(string s)
    {
        var reg = Regex.Match(s, _bracketsPattern);
        return reg.Success ? reg.Groups[1].Value : null;
    }
}