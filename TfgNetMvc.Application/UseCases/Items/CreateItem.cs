using TfgNetMvc.Application.DTOs.Items;
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

    public async Task<int> ExecuteAsync(CreateItemDto dto, CancellationToken cancellationToken = default)
    {
        var item = new Item(dto.Name, dto.Description, dto.Stock, dto.CategoryId, dto.WarehouseLocationId);

        await _repository.AddAsync(item, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}
