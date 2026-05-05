using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.UseCases.Items;

public class GetItemById
{
    private readonly IItemRepository _repository;

    public GetItemById(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Item?> ExecuteAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
