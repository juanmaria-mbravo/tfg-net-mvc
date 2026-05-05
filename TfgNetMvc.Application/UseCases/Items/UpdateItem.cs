using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Items;

public class UpdateItem
{
    private readonly IItemRepository _repository;

    public UpdateItem(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id, string name, string? description, int stock)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item is null)
            return false;

        item.Update(name, description, stock);

        _repository.Update(item);
        await _repository.SaveChangesAsync();

        return true;
    }
}
