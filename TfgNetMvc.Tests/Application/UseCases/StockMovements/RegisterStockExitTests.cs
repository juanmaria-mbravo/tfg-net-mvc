using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.UseCases.StockMovements;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.StockMovements;

public class RegisterStockExitTests
{
    [Fact]
    public async Task ExecuteAsync_ValidExit_DecreasesStock()
    {
        var itemRepo = new FakeItemRepository();
        var movementRepo = new FakeStockMovementRepository();
        var item = new Item("Widget", null, 20);
        await itemRepo.AddAsync(item);

        var useCase = new RegisterStockExit(itemRepo, movementRepo);
        var dto = new RegisterStockExitDto { ItemId = item.Id, Quantity = 8 };

        await useCase.ExecuteAsync(dto);

        var updated = await itemRepo.GetByIdAsync(item.Id);
        Assert.Equal(12, updated!.Stock);
    }

    [Fact]
    public async Task ExecuteAsync_InsufficientStock_Throws()
    {
        var itemRepo = new FakeItemRepository();
        var movementRepo = new FakeStockMovementRepository();
        var item = new Item("Widget", null, 5);
        await itemRepo.AddAsync(item);

        var useCase = new RegisterStockExit(itemRepo, movementRepo);
        var dto = new RegisterStockExitDto { ItemId = item.Id, Quantity = 10 };

        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(dto));
    }

    [Fact]
    public async Task ExecuteAsync_ItemNotFound_Throws()
    {
        var itemRepo = new FakeItemRepository();
        var movementRepo = new FakeStockMovementRepository();
        var useCase = new RegisterStockExit(itemRepo, movementRepo);
        var dto = new RegisterStockExitDto { ItemId = 99, Quantity = 5 };

        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(dto));
    }
}
