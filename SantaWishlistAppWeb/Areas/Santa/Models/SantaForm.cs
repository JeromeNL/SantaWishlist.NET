using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SantaWishlist.Data.Models;

public class SantaForm
{
    [Required(ErrorMessage = "Je moet minimaal 1 naam invullen!")]
    [DisplayName("Alle namen")]
    [MinLength(3, ErrorMessage = "Je moet minimaal 1 naam invullen!")]
    public string Names { get; set; }

    [DisplayName("Braaf geweest?")]
    public bool WellBehaved { get; set; }

    [DisplayName("Verzin een wachtwoord")]
    [Required(ErrorMessage = "Vul een wachtwoord in")]
    [MaxLength(20), MinLength(3, ErrorMessage = "Vul een wachtwoord in van minimaal 3 tekens")]
    [RegularExpression(@"^[a-z]+$", ErrorMessage = "Gebruik alleen kleine letters.")]
    public string GivenPassword { get; set; }
}