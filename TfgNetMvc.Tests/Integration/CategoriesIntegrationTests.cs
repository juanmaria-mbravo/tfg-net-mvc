using System.Net;
using TfgNetMvc.Tests.Integration.Factories;

namespace TfgNetMvc.Tests.Integration;

public class CategoriesIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CategoriesIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Categories_Index_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/Categories");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Categories_Create_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/Categories/Create");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Categories_Details_WithNonExistingId_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/Categories/Details/999999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
