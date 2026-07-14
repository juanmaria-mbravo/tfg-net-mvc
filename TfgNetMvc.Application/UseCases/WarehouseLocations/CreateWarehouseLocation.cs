using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.UseCases.WarehouseLocations;

public class CreateWarehouseLocation(IWarehouseLocationRepository repository)
{
    public async Task<int> ExecuteAsync(CreateWarehouseLocationDto dto)
    {
        var location = new WarehouseLocation(dto.Name, dto.Description);
        await repository.AddAsync(location);
        await repository.SaveChangesAsync();
        return location.Id;
    }
}
