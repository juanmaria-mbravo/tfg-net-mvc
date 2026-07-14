using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.WarehouseLocations;

public class UpdateWarehouseLocation(IWarehouseLocationRepository repository)
{
    public async Task<bool> ExecuteAsync(UpdateWarehouseLocationDto dto)
    {
        var location = await repository.GetByIdAsync(dto.Id);
        if (location is null) return false;

        location.Update(dto.Name, dto.Description);
        repository.Update(location);
        await repository.SaveChangesAsync();
        return true;
    }
}
