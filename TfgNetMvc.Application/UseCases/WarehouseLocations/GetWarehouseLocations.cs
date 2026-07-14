using AutoMapper;
using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.WarehouseLocations;

public class GetWarehouseLocations(IWarehouseLocationRepository repository, IMapper mapper)
{
    public async Task<List<WarehouseLocationDto>> ExecuteAsync()
    {
        var locations = await repository.GetAllAsync();
        return mapper.Map<List<WarehouseLocationDto>>(locations);
    }
}
