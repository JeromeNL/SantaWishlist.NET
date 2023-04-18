using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Program = Microsoft.VisualStudio.TestPlatform.TestHost.Program;

namespace TestSanta;
using Xunit;
using SantaWishlistApp;
using Microsoft.AspNetCore.Mvc.Testing;

public class UnitTest1 : IClassFixture<WebApplicationFactory<>>
{
    private readonly WebApplicationFactory<Program> _factory;
    
    public UnitTest1(string environment = "Development")
    {
        _environment = environment;
    }
    
    [Fact]
    public async void TestIndexLoads()
    {
        //Arrange
        var client = _factory.CreateClient();
        
        //Act
        var response = await client.GetAsync("/Home/Index");
        int code = (int)response.StatusCode;
        //Assert
        Assert.Equal(200, code);
    }
}