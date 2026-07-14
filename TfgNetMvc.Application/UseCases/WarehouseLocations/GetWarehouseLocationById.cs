using AutoMapper;
using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.WarehouseLocations;

public class GetWarehouseLocationById(IWarehouseLocationRepository repository, IMapper mapper)
{
    public async Task<WarehouseLocationDto?> ExecuteAsync(int id)
    {
        var location = await repository.GetByIdAsync(id);
        return location is null ? null : mapper.Map<WarehouseLocationDto>(location);
    }
}
