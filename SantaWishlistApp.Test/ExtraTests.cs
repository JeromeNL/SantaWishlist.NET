using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using SantasWishlist.Domain;
using SantaWishlist.Data;
using SantaWishlist.Data.DAL;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Utils;
using Xunit;

namespace SantaWishlistApp.Test;

public class ExtraTests
{
    [Fact]
    public void ReflectionHelper_ReturnsCorrectName()
    {
        var name = ReflectionHelper.PropertyName<Gift>(x => x.Name);
        Assert.Equal("Name", name);
    }
    
    [Fact]
    public void GetDuplicatedNames_ShouldBeEmpty()
    {
        var input = "Piet58403,Kees43092,Jan84832";
        var repo = A.Fake<SantaRepository>();

        var result = repo.GetDuplicatedNames(input);
        
        Assert.Empty(result);
    }

    [Fact]
    public void GetPossibleGifts_ShouldNotBeNull()
    {
        var context = A.Fake<SantaDbContext>();
        var userManager = A.Fake<UserManager<SantaUser>>();
        var giftRepo = A.Fake<IGiftRepository>();
        var repo = new SantaRepository(context, userManager, giftRepo);

        var result = repo.GetPossibleGifts();
        
        Assert.NotNull(result);
    }
}