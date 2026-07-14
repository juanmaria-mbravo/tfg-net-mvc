using Microsoft.EntityFrameworkCore;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Infrastructure.Persistence;

namespace TfgNetMvc.Infrastructure.Repositories;

public class WarehouseLocationRepository(AppDbContext context) : IWarehouseLocationRepository
{
    public async Task<List<WarehouseLocation>> GetAllAsync()
        => await context.WarehouseLocations.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

    public async Task<WarehouseLocation?> GetByIdAsync(int id)
        => await context.WarehouseLocations.FindAsync(id);

    public async Task AddAsync(WarehouseLocation location)
        => await context.WarehouseLocations.AddAsync(location);

    public void Update(WarehouseLocation location)
        => context.WarehouseLocations.Update(location);

    public void Delete(WarehouseLocation location)
        => context.WarehouseLocations.Remove(location);

    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}
