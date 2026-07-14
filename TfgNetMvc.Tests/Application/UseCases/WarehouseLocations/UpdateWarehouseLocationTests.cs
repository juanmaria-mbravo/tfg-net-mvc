using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.WarehouseLocations;

public class UpdateWarehouseLocationTests
{
    private readonly FakeWarehouseLocationRepository _repo = new();

    [Fact]
    public async Task Execute_ExistingLocation_ReturnsTrue()
    {
        await _repo.AddAsync(new WarehouseLocation("Old Name", null));
        var useCase = new UpdateWarehouseLocation(_repo);
        var dto = new UpdateWarehouseLocationDto { Id = 1, Name = "New Name", Description = "Desc" };

        var result = await useCase.ExecuteAsync(dto);

        Assert.True(result);
    }

    [Fact]
    public async Task Execute_NonExistingLocation_ReturnsFalse()
    {
        var useCase = new UpdateWarehouseLocation(_repo);
        var dto = new UpdateWarehouseLocationDto { Id = 99, Name = "X", Description = null };

        var result = await useCase.ExecuteAsync(dto);

        Assert.False(result);
    }
}
