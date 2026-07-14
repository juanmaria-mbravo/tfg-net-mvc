using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.WarehouseLocations;

public class CreateWarehouseLocationTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidData_CreatesLocation()
    {
        var repository = new FakeWarehouseLocationRepository();
        var useCase = new CreateWarehouseLocation(repository);
        var dto = new CreateWarehouseLocationDto { Name = "Zone A", Description = null };

        var id = await useCase.ExecuteAsync(dto);
        var created = await repository.GetByIdAsync(id);

        Assert.NotNull(created);
        Assert.Equal("Zone A", created.Name);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyName_ThrowsArgumentException()
    {
        var repository = new FakeWarehouseLocationRepository();
        var useCase = new CreateWarehouseLocation(repository);
        var dto = new CreateWarehouseLocationDto { Name = "" };

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
    }
}
