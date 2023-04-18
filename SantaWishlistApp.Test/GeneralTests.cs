using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using SantaWishlist.Data.DAL;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Areas.Child.Controllers;
using Xunit;

namespace SantaWishlistApp.Test;

public class GeneralTests
{
    [Theory]
    [InlineData("")]
    [InlineData("/Identity/Account/Login")]
    public async Task TestAllPublicPages_ShouldReturn200(string url)
    {
        //Arrange
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        //Act
        var response = await httpClient.GetAsync(url);
        int code = (int)response.StatusCode;
        
        //Assert
        Assert.Equal(200, code);
    }
    
    
    [Theory]
    [InlineData("/Child/Home/Index")]
    [InlineData("/Santa/Home/Index")]
    public async Task TestPrivatePages_ShouldReturn302(string url)
    {
        //Arrange
        var webAppFactory = new WebApplicationFactory<Program>();
        var httpClient = webAppFactory.CreateDefaultClient();

        //Act
        var response = await httpClient.GetAsync(url);
        int code = (int)response.StatusCode;

        //Assert
        Assert.Equal(302, code);
    }


    [Fact]
    public void SendList_ShouldReturnView()
    {
        var signInManager = A.Fake<SignInManager<SantaUser>>();
        var childRepo = A.Fake<ChildRepository>();
        var controller = new HomeController(childRepo, signInManager);
        
        var result = controller.SendList().Result as ViewResult;
        Assert.Equal("SendList", result?.ViewName);

    }
}