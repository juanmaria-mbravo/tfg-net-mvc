using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.WarehouseLocations;

public class DeleteWarehouseLocation(IWarehouseLocationRepository repository)
{
    public async Task<bool> ExecuteAsync(int id)
    {
        var location = await repository.GetByIdAsync(id);
        if (location is null) return false;

        repository.Delete(location);
        await repository.SaveChangesAsync();
        return true;
    }
}
