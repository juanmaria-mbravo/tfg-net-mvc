using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Application.Fakes;

public class FakeWarehouseLocationRepository : IWarehouseLocationRepository
{
    private readonly List<WarehouseLocation> _store = [];
    private int _nextId = 1;

    public Task<List<WarehouseLocation>> GetAllAsync()
        => Task.FromResult(_store.ToList());

    public Task<WarehouseLocation?> GetByIdAsync(int id)
        => Task.FromResult(_store.FirstOrDefault(x => x.Id == id));

    public Task AddAsync(WarehouseLocation location)
    {
        typeof(WarehouseLocation)
            .GetProperty(nameof(WarehouseLocation.Id))!
            .SetValue(location, _nextId++);
        _store.Add(location);
        return Task.CompletedTask;
    }

    public void Update(WarehouseLocation location) { }

    public void Delete(WarehouseLocation location)
        => _store.Remove(location);

    public Task SaveChangesAsync()
        => Task.CompletedTask;
}
