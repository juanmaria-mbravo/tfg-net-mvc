using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Domain.Enums;

namespace TfgNetMvc.Application.UseCases.StockMovements;

public class RegisterStockEntry(IItemRepository itemRepository, IStockMovementRepository movementRepository)
{
    public async Task<int> ExecuteAsync(RegisterStockEntryDto dto)
    {
        var item = await itemRepository.GetByIdAsync(dto.ItemId)
            ?? throw new InvalidOperationException($"Item {dto.ItemId} not found.");

        var previousStock = item.Stock;
        item.AddStock(dto.Quantity);
        itemRepository.Update(item);

        var movement = new StockMovement(
            item.Id, StockMovementType.Entry, dto.Quantity,
            previousStock, item.Stock, dto.Reason,
            dto.SupplierId, dto.WarehouseLocationId);

        await movementRepository.AddAsync(movement);
        await movementRepository.SaveChangesAsync();

        return movement.Id;
    }
}
