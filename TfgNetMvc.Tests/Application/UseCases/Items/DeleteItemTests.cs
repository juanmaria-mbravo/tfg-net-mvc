using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Items;

public class DeleteItemTests
{
    [Fact]
    public async Task ExecuteAsync_WithExistingItem_ShouldDeleteItem()
    {
        var repository = new FakeItemRepository();
        var item = new Item("Laptop", "Work laptop", 10);
        await repository.AddAsync(item);

        var useCase = new DeleteItem(repository);

        var result = await useCase.ExecuteAsync(item.Id);

        var deletedItem = await repository.GetByIdAsync(item.Id);

        Assert.True(result);
        Assert.Null(deletedItem);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistingItem_ShouldReturnFalse()
    {
        var repository = new FakeItemRepository();
        var useCase = new DeleteItem(repository);

        var result = await useCase.ExecuteAsync(999);

        Assert.False(result);
    }
}
