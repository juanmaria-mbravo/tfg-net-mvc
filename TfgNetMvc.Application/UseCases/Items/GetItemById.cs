using AutoMapper;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Items;

public class GetItemById
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public GetItemById(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ItemDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);

        if (item is null)
            return null;

        return _mapper.Map<ItemDto>(item);
    }
}
