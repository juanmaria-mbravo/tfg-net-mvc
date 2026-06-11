using AutoMapper;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Items;

public class GetItems
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public GetItems(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ItemDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<ItemDto>>(items);
    }
}
