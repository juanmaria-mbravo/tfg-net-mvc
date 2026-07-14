using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Application.Fakes;

public class FakeStockMovementRepository : IStockMovementRepository
{
    private readonly List<StockMovement> _store = [];
    private int _nextId = 1;

    public Task<List<StockMovement>> GetAllAsync()
        => Task.FromResult(_store.OrderByDescending(m => m.MovementDateUtc).ToList());

    public Task<List<StockMovement>> GetByItemIdAsync(int itemId)
        => Task.FromResult(_store.Where(m => m.ItemId == itemId).ToList());

    public Task AddAsync(StockMovement movement)
    {
        typeof(StockMovement)
            .GetProperty(nameof(StockMovement.Id))!
            .SetValue(movement, _nextId++);
        _store.Add(movement);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
        => Task.CompletedTask;
}
