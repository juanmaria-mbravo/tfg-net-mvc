using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Items;

public class CreateItemTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidData_CreatesItem()
    {
        var repository = new FakeItemRepository();
        var useCase = new CreateItem(repository);

        var dto = new CreateItemDto
        {
            Name = "Test item",
            Description = "Test description",
            Stock = 10
        };

        var id = await useCase.ExecuteAsync(dto);

        var createdItem = await repository.GetByIdAsync(id);

        Assert.NotNull(createdItem);
        Assert.Equal("Test item", createdItem.Name);
        Assert.Equal("Test description", createdItem.Description);
        Assert.Equal(10, createdItem.Stock);
    }

    [Fact]
    public async Task ExecuteAsync_WithWarehouseLocationId_AssignsLocation()
    {
        var repository = new FakeItemRepository();
        var useCase = new CreateItem(repository);

        var dto = new CreateItemDto
        {
            Name = "Shelf item",
            Description = null,
            Stock = 5,
            WarehouseLocationId = 3
        };

        var id = await useCase.ExecuteAsync(dto);
        var createdItem = await repository.GetByIdAsync(id);

        Assert.NotNull(createdItem);
        Assert.Equal(3, createdItem.WarehouseLocationId);
    }

    [Fact]
    public async Task ExecuteAsync_WithoutWarehouseLocationId_LocationIsNull()
    {
        var repository = new FakeItemRepository();
        var useCase = new CreateItem(repository);

        var dto = new CreateItemDto
        {
            Name = "Unlocated item",
            Description = null,
            Stock = 2
        };

        var id = await useCase.ExecuteAsync(dto);
        var createdItem = await repository.GetByIdAsync(id);

        Assert.NotNull(createdItem);
        Assert.Null(createdItem.WarehouseLocationId);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyName_ThrowsArgumentException()
    {
        var repository = new FakeItemRepository();
        var useCase = new CreateItem(repository);

        var dto = new CreateItemDto
        {
            Name = "",
            Description = "Invalid item",
            Stock = 10
        };

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
    }
}