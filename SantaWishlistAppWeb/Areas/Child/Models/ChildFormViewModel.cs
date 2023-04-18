using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SantaWishlist.Data.Models.Enums;

namespace SantaWishlist.Data.Models;

public class ChildFormViewModel
{
    [DisplayName("Hoe oud ben je?")]
    [Required(ErrorMessage = "Laat de Kerstman even weten hoe oud je bent.")]
    [RegularExpression("^[1-9][0-9]?$|^100$", ErrorMessage = "Geef een leeftijd op tussen 1 en 100 jaar.")]
    public int Age { get; set; }

    [DisplayName("Hoe heb jij je gedragen?")]
    [Required(ErrorMessage = "De Kerstman weet alles, dus eerlijk invullen")]
    public BehavedEnum Behaved { get; set; }

    [DisplayName("Uitleg")]
    [MinLength(25, ErrorMessage = "Deze uitleg is wel erg kort..")]
    [Required(ErrorMessage = "Ook even uitleggen waarom, he?")]
    public string Explanation { get; set; }
}