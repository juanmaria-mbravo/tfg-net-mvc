using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Items;

public class CreateItemTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidData_ShouldCreateItemAndReturnId()
    {
        var repository = new FakeItemRepository();
        var useCase = new CreateItem(repository);

        var id = await useCase.ExecuteAsync("Laptop", "Work laptop", 10);

        var item = await repository.GetByIdAsync(id);

        Assert.NotNull(item);
        Assert.Equal("Laptop", item.Name);
        Assert.Equal("Work laptop", item.Description);
        Assert.Equal(10, item.Stock);
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidName_ShouldThrowArgumentException()
    {
        var repository = new FakeItemRepository();
        var useCase = new CreateItem(repository);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.ExecuteAsync("", "Description", 10));
    }
}
