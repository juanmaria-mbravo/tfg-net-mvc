using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using TfgNetMvc.Tests.Integration.Factories;

namespace TfgNetMvc.Tests.Integration;

public class AuthorizationTests : IClassFixture<AnonymousWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthorizationTests(AnonymousWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Theory]
    [InlineData("/Items")]
    [InlineData("/Categories")]
    [InlineData("/Suppliers")]
    [InlineData("/WarehouseLocations")]
    [InlineData("/StockMovements")]
    public async Task Get_ProtectedRoute_WithoutAuth_RedirectsToLogin(string url)
    {
        var response = await _client.GetAsync(url);

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Contains("Login", response.Headers.Location?.ToString() ?? "");
    }

    [Fact]
    public async Task Get_Home_WithoutAuth_ReturnsOk()
    {
        var response = await _client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
