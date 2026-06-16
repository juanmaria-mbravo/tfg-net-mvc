using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.UseCases.Categories;

public class CreateCategory
{
    private readonly ICategoryRepository _repository;

    public CreateCategory(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> ExecuteAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var category = new Category(dto.Name, dto.Description);

        await _repository.AddAsync(category, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
