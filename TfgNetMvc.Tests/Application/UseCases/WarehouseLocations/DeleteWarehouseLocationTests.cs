using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.WarehouseLocations;

public class DeleteWarehouseLocationTests
{
    private readonly FakeWarehouseLocationRepository _repo = new();

    [Fact]
    public async Task Execute_ExistingLocation_ReturnsTrue()
    {
        await _repo.AddAsync(new WarehouseLocation("Zone A", null));
        var useCase = new DeleteWarehouseLocation(_repo);

        var result = await useCase.ExecuteAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task Execute_NonExistingLocation_ReturnsFalse()
    {
        var useCase = new DeleteWarehouseLocation(_repo);

        var result = await useCase.ExecuteAsync(99);

        Assert.False(result);
    }
}
