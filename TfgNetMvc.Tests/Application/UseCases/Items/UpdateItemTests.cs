using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Items;

public class UpdateItemTests
{
    [Fact]
    public async Task ExecuteAsync_WhenItemExists_UpdatesItem()
    {
        var repository = new FakeItemRepository();
        var existingItem = new Item("Original item", "Original description", 5);

        await repository.AddAsync(existingItem);
        await repository.SaveChangesAsync();

        var useCase = new UpdateItem(repository);

        var dto = new UpdateItemDto
        {
            Id = existingItem.Id,
            Name = "Updated item",
            Description = "Updated description"
        };

        var result = await useCase.ExecuteAsync(dto);

        var updatedItem = await repository.GetByIdAsync(existingItem.Id);

        Assert.True(result);
        Assert.NotNull(updatedItem);
        Assert.Equal("Updated item", updatedItem.Name);
        Assert.Equal("Updated description", updatedItem.Description);
        Assert.Equal(5, updatedItem.Stock);
    }

    [Fact]
    public async Task ExecuteAsync_WhenItemDoesNotExist_ReturnsFalse()
    {
        var repository = new FakeItemRepository();
        var useCase = new UpdateItem(repository);

        var dto = new UpdateItemDto
        {
            Id = 999,
            Name = "Updated item",
            Description = "Updated description"
        };

        var result = await useCase.ExecuteAsync(dto);

        Assert.False(result);
    }
}