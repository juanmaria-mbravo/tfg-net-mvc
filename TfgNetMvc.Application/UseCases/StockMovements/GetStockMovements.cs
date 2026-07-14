using AutoMapper;
using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.StockMovements;

public class GetStockMovements(IStockMovementRepository repository, IMapper mapper)
{
    public async Task<List<StockMovementDto>> ExecuteAsync()
    {
        var movements = await repository.GetAllAsync();
        return mapper.Map<List<StockMovementDto>>(movements);
    }
}
