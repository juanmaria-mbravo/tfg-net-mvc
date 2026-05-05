using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Items;

public class DeleteItem
{
    private readonly IItemRepository _repository;

    public DeleteItem(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item is null)
            return false;

        _repository.Delete(item);
        await _repository.SaveChangesAsync();

        return true;
    }
}
