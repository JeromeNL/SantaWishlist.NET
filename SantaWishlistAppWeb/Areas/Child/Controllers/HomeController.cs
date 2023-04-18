using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantaWishlist.Data.DAL.Interfaces;
using SantaWishlist.Data.Models;
using SantaWishlist.Data.Models.Enums;
using SantaWishlistApp.Areas.Child.Models;
using SantaWishlistApp.Extensions;

namespace SantaWishlistApp.Areas.Child.Controllers;

[Area(nameof(Role.Child))]
[Authorize(Roles = nameof(Role.Child))]
public class HomeController : Controller
{
    private readonly IChildRepository _childRepo;
    private readonly SignInManager<SantaUser> _signInManager;

    public HomeController(IChildRepository childRepo, SignInManager<SantaUser> SignInManager)
    {
        _childRepo = childRepo;
        _signInManager = SignInManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(ChildFormViewModel childFormViewModel)
    {
        // Check model state errors
        if (!ModelState.IsValid)
            return View(childFormViewModel);

        return await GiftsPage(childFormViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GiftsPage(ChildFormViewModel? childFormViewModel)
    {
        // Check whether user has submitted form
        if (childFormViewModel == null)
            return RedirectToAction(nameof(Index));

        // Group gifts by category
        var giftsByCategory = await _childRepo.GetPresentsSortedByCategory();

        // Create view model
        var vm = new GiftChooseViewModel
        {
            ChildFormViewModel = childFormViewModel,
            CategoryGiftsDict = giftsByCategory.ToCheckboxModel()
        };
        return View("GiftsPage", vm);
    }

    [HttpPost]
    public async Task<IActionResult> GiftsPage(GiftChooseViewModel givenModel)
    {
        // Check model state errors
        if (!ModelState.IsValid)
            return View(givenModel);

        var username = HttpContext.User.Identity.Name!;

        await _childRepo.SendWishlistToSanta(username, givenModel.ToWishlist());

        await _childRepo.LockUser(username);


        return View("ConfirmPage", givenModel);
    }

    [HttpGet]
    public async Task<IActionResult> SendList()
    {
        await _signInManager.SignOutAsync();
        return View("SendList");
    }
}