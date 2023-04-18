using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantaWishlist.Data.DAL.Interfaces;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;

namespace SantaWishlistApp.Areas.Santa.Controllers;

[Area(nameof(Role.Santa))]
[Authorize(Roles = nameof(Role.Santa))]
public class HomeController : Controller
{
    private readonly UserManager<SantaUser> _userManager;
    private readonly ISantaRepository _repo;

    public HomeController(UserManager<SantaUser> userManager, ISantaRepository repo)
    {
        _userManager = userManager;
        _repo = repo;
    }

    // GET: Santa
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ViewResult> Index(SantaForm santaForm)
    {
        if (santaForm.Names == null)
        {
            return View();
        }

        var duplicatesNames = _repo.GetDuplicatedNames(santaForm.Names);
        if (duplicatesNames.Any())
        {
            ModelState.AddModelError("duplicated",
                "Een naam mag maximaal 1 keer voorkomen! De volgende namen komen meerdere keren voor: " +
                string.Join(", ", duplicatesNames));
            return View(santaForm);
        }

        foreach (var name in santaForm.Names.Split(',').Select(x => x.Trim()))
        {
            var childUser = await _repo.CreateNewChildUser(name, santaForm.WellBehaved, santaForm.GivenPassword);
            var resultUser = await _repo.GetExistingUser(childUser);
            if (resultUser != null)
            {
                ModelState.AddModelError("Error", "Deze naam is al eerder gebruikt");
                return View(santaForm);
            }

            await _repo.AddNewChildUser(childUser, Role.Child);
        }

        return View("Success", santaForm);
    }
}
