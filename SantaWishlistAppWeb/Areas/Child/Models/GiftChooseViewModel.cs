using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SantasWishlist.Domain;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Areas.Child.Models.Binders;
using SantaWishlistApp.Areas.Child.Models.Validations;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Models;

public class GiftChooseViewModel : IValidatableObject
{
    [DisplayName("Andere wensen")]
    public string? OtherWishes { get; set; }

    public List<string> OtherWishesList =>
        OtherWishes != null ? OtherWishes.Split(',').Select(x => x.Trim()).ToList() : new List<string>();

    public ChildFormViewModel ChildFormViewModel { get; set; }

    [ModelBinder(BinderType = typeof(GiftChooseBinder))]
    public Dictionary<GiftCategory, List<GiftCheckboxModel>> CategoryGiftsDict { get; set; }

    private Dictionary<GiftCategory, List<GiftCheckboxModel>> _selectedGifts;

    public Dictionary<GiftCategory, List<GiftCheckboxModel>> SelectedGifts
    {
        get
        {
            if (_selectedGifts == null)
            {
                _selectedGifts = CategoryGiftsDict
                    .ToDictionary(x => x.Key, x => x.Value.Where(x => x.IsChecked).ToList());
            }

            return _selectedGifts;
        }
        set { _selectedGifts = value; }
    }


    public IEnumerable<ValidationResult> Validate(ValidationContext validation)
    {
        // Rule 1
        if (this.ValidateWith<MaxPresentsPerCategory>(validation))
            yield return new ValidationResult("Je mag niet zoveel cadeaus per categorie kiezen");

        // Rule 2
        if (this.ValidateWith<LegoAndKnex>(validation))
            yield return new ValidationResult("Je mag geen lego en knex samen kiezen");

        // Rule 3
        if (this.ValidateWith<AgeRelatedGifts>(validation))
            yield return new ValidationResult("Jouw cadeau's passen niet echt bij jouw leeftijd, kies wat anders.");

        // Rule 6
        if (this.ValidateWith<NightLight>(validation))
            yield return new ValidationResult(
                "Als je het nachtlampje kiest, moet je ook ondergoed op je lijstje zetten.");

        // Rule 7
        if (this.ValidateWith<SelectedInstrument>(validation))
            yield return new ValidationResult("Je hebt een muziekinstrument gekozen, kies ook oordopjes.");

        // Rule 8
        if (this.ValidateWith<CustomPresentExists>(validation))
            yield return new ValidationResult("Euhmm, jouw eigen gekozen caudeau staat gewoon in de lijst, hoor.");

        // Rule 9
        if (this.ValidateWith<CustomRule>(validation))
            yield return new ValidationResult(
                "Als je voor de Spelcomputer of het Computerspel kiest, moet je ook bijles kiezen.");

        // Rule 10
        if (this.ValidateWith<ErkansExtraRule>(validation))
            yield return new ValidationResult("Als je een boek kiest, moet je ook het nachtlampje kiezen.");
    }
}