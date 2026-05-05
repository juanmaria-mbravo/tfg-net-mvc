using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.UseCases.Items;

public class CreateItem
{
    private readonly IItemRepository _repository;

    public CreateItem(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> ExecuteAsync(string name, string? description, int stock)
    {
        var item = new Item(name, description, stock);

        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();

        return item.Id;
    }
}
