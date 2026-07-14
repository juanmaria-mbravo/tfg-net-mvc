using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Interfaces.Repositories;

public interface IWarehouseLocationRepository
{
    Task<List<WarehouseLocation>> GetAllAsync();
    Task<WarehouseLocation?> GetByIdAsync(int id);
    Task AddAsync(WarehouseLocation location);
    void Update(WarehouseLocation location);
    void Delete(WarehouseLocation location);
    Task SaveChangesAsync();
}
