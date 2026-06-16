using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Items;

public class UpdateItem
{
    private readonly IItemRepository _repository;

    public UpdateItem(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(UpdateItemDto dto, CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(dto.Id, cancellationToken);

        if (item is null)
            return false;

        item.Update(dto.Name, dto.Description, dto.Stock, dto.CategoryId);

        _repository.Update(item);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
