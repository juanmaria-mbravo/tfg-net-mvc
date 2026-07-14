using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Interfaces.Repositories;

public interface IStockMovementRepository
{
    Task<List<StockMovement>> GetAllAsync();
    Task<List<StockMovement>> GetByItemIdAsync(int itemId);
    Task AddAsync(StockMovement movement);
    Task SaveChangesAsync();
}
