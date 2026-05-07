using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Items;

public class UpdateItemTests
{
    [Fact]
    public async Task ExecuteAsync_WithExistingItem_ShouldUpdateItem()
    {
        var repository = new FakeItemRepository();
        var item = new Item("Laptop", "Work laptop", 10);
        await repository.AddAsync(item);

        var useCase = new UpdateItem(repository);

        var result = await useCase.ExecuteAsync(item.Id, "Monitor", "Updated description", 5);

        var updatedItem = await repository.GetByIdAsync(item.Id);

        Assert.True(result);
        Assert.NotNull(updatedItem);
        Assert.Equal("Monitor", updatedItem.Name);
        Assert.Equal("Updated description", updatedItem.Description);
        Assert.Equal(5, updatedItem.Stock);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistingItem_ShouldReturnFalse()
    {
        var repository = new FakeItemRepository();
        var useCase = new UpdateItem(repository);

        var result = await useCase.ExecuteAsync(999, "Monitor", "Description", 5);

        Assert.False(result);
    }
}
