using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Category category, CancellationToken cancellationToken = default);

    void Update(Category category);

    void Delete(Category category);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
