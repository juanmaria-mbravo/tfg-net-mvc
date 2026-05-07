using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Application.Fakes;

public class FakeItemRepository : IItemRepository
{
    private readonly List<Item> _items = new();
    private int _nextId = 1;

    public Task<List<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_items.ToList());
    }

    public Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(item);
    }

    public Task AddAsync(Item item, CancellationToken cancellationToken = default)
    {
        SetId(item, _nextId++);
        _items.Add(item);

        return Task.CompletedTask;
    }

    public void Update(Item item)
    {
    }

    public void Delete(Item item)
    {
        _items.Remove(item);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    private static void SetId(Item item, int id)
    {
        var property = typeof(Item).GetProperty(nameof(Item.Id));

        if (property is null)
            throw new InvalidOperationException("Item Id property was not found.");

        property.SetValue(item, id);
    }
}
