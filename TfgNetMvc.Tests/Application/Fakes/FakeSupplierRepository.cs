using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Application.Fakes;

public class FakeSupplierRepository : ISupplierRepository
{
    private readonly List<Supplier> _suppliers = new();
    private int _nextId = 1;

    public Task<List<Supplier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_suppliers.ToList());
    }

    public Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var supplier = _suppliers.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(supplier);
    }

    public Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        SetId(supplier, _nextId++);
        _suppliers.Add(supplier);

        return Task.CompletedTask;
    }

    public void Update(Supplier supplier)
    {
    }

    public void Delete(Supplier supplier)
    {
        _suppliers.Remove(supplier);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    private static void SetId(Supplier supplier, int id)
    {
        var property = typeof(Supplier).GetProperty(nameof(Supplier.Id));

        if (property is null)
            throw new InvalidOperationException("Supplier Id property was not found.");

        property.SetValue(supplier, id);
    }
}
