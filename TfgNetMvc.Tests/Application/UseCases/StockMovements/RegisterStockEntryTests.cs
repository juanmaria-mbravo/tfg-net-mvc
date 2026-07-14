using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.UseCases.StockMovements;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.StockMovements;

public class RegisterStockEntryTests
{
    [Fact]
    public async Task ExecuteAsync_ValidEntry_IncreasesStock()
    {
        var itemRepo = new FakeItemRepository();
        var movementRepo = new FakeStockMovementRepository();
        var item = new Item("Widget", null, 10);
        await itemRepo.AddAsync(item);

        var useCase = new RegisterStockEntry(itemRepo, movementRepo);
        var dto = new RegisterStockEntryDto { ItemId = item.Id, Quantity = 5, Reason = "Restock" };

        await useCase.ExecuteAsync(dto);

        var updated = await itemRepo.GetByIdAsync(item.Id);
        Assert.Equal(15, updated!.Stock);
    }

    [Fact]
    public async Task ExecuteAsync_ValidEntry_CreatesMovementRecord()
    {
        var itemRepo = new FakeItemRepository();
        var movementRepo = new FakeStockMovementRepository();
        var item = new Item("Widget", null, 10);
        await itemRepo.AddAsync(item);

        var useCase = new RegisterStockEntry(itemRepo, movementRepo);
        var dto = new RegisterStockEntryDto { ItemId = item.Id, Quantity = 5 };

        await useCase.ExecuteAsync(dto);

        var movements = await movementRepo.GetAllAsync();
        Assert.Single(movements);
        Assert.Equal(10, movements[0].PreviousStock);
        Assert.Equal(15, movements[0].NewStock);
    }

    [Fact]
    public async Task ExecuteAsync_ItemNotFound_Throws()
    {
        var itemRepo = new FakeItemRepository();
        var movementRepo = new FakeStockMovementRepository();
        var useCase = new RegisterStockEntry(itemRepo, movementRepo);
        var dto = new RegisterStockEntryDto { ItemId = 99, Quantity = 5 };

        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ExecuteAsync(dto));
    }
}
