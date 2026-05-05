using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.UseCases.Items;

public class GetItems
{
    private readonly IItemRepository _repository;

    public GetItems(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Item>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
