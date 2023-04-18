using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SantaWishlist.Data.Models;

[Table("SantaUsers")]
public class SantaUser : IdentityUser
{
}