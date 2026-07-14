using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Interfaces.Repositories;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default);

    void Update(Supplier supplier);

    void Delete(Supplier supplier);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
