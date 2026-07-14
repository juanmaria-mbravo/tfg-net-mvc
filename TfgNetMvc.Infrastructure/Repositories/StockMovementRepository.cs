using Microsoft.EntityFrameworkCore;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Infrastructure.Persistence;

namespace TfgNetMvc.Infrastructure.Repositories;

public class StockMovementRepository(AppDbContext context) : IStockMovementRepository
{
    public async Task<List<StockMovement>> GetAllAsync()
        => await context.StockMovements
            .AsNoTracking()
            .Include(m => m.Item)
            .Include(m => m.Supplier)
            .Include(m => m.WarehouseLocation)
            .OrderByDescending(m => m.MovementDateUtc)
            .ToListAsync();

    public async Task<List<StockMovement>> GetByItemIdAsync(int itemId)
        => await context.StockMovements
            .AsNoTracking()
            .Include(m => m.Item)
            .Include(m => m.Supplier)
            .Include(m => m.WarehouseLocation)
            .Where(m => m.ItemId == itemId)
            .OrderByDescending(m => m.MovementDateUtc)
            .ToListAsync();

    public async Task AddAsync(StockMovement movement)
        => await context.StockMovements.AddAsync(movement);

    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
