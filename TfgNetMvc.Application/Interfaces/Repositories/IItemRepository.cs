using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Interfaces.Repositories;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Item item, CancellationToken cancellationToken = default);

    void Update(Item item);

    void Delete(Item item);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
