using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TfgNetMvc.Tests.Integration;

public class ItemsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ItemsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Items_Index_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/Items");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Items_Create_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/Items/Create");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Items_Details_WithNonExistingId_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/Items/Details/999999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
