using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantaWishlist.Data.DAL;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Areas.Child.Controllers;
using SantaWishlistApp.Areas.Child.Models;
using Xunit;

namespace SantaWishlistApp.Test;

public class ControllerTests
{
    [Fact]
    public void IndexSantaForm_ShouldReturnNotNull()
    {
        var santaController = A.Fake<Areas.Santa.Controllers.HomeController>();
        var santaForm = A.Fake<SantaForm>();

        var result = santaController.Index(santaForm);
        var resultWithArgument = santaController.Index();
        
        Assert.NotNull(result);
        Assert.NotNull(resultWithArgument);
    }


    [Fact]
    public void IndexChildForm_ShouldReturnNotNull()
    {
        var santaController = A.Fake<HomeController>();
        var childFormVm = A.Fake<ChildFormViewModel>();

        var result = santaController.Index(childFormVm);
        var resultWithArgument = santaController.Index();
        
        Assert.NotNull(result);
        Assert.NotNull(resultWithArgument);
    }
    
    
    [Fact]
    public async Task GiftsPage_ShouldReturnView()
    {
        var signInManager = A.Fake<SignInManager<SantaUser>>();
        var childRepo = A.Fake<ChildRepository>();
        var controller = new HomeController(childRepo, signInManager);
        var childForm = A.Fake<ChildFormViewModel>();

        var resultChild = await controller.GiftsPage(childForm) as ViewResult;
        Assert.Equal("GiftsPage", resultChild?.ViewName);
    }


    [Fact]
    public async Task GiftPageWithParameter_ShouldReturnView()
    {
        var signInManager = A.Fake<SignInManager<SantaUser>>();
        var childRepo = A.Fake<ChildRepository>();
        var controller = new HomeController(childRepo, signInManager);
        var childForm = A.Fake<ChildFormViewModel>();

        var result = await controller.GiftsPage(childForm) as ViewResult;
        var product = (GiftChooseViewModel?)result?.ViewData.Model;
        Assert.NotNull(product);
    }
}