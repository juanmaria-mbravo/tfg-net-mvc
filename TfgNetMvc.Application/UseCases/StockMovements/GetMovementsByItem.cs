using AutoMapper;
using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.StockMovements;

public class GetMovementsByItem(IStockMovementRepository repository, IMapper mapper)
{
    public async Task<List<StockMovementDto>> ExecuteAsync(int itemId)
    {
        var movements = await repository.GetByItemIdAsync(itemId);
        return mapper.Map<List<StockMovementDto>>(movements);
    }
}
