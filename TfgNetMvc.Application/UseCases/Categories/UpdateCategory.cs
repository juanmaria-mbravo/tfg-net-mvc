using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Categories;

public class UpdateCategory
{
    private readonly ICategoryRepository _repository;

    public UpdateCategory(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(UpdateCategoryDto dto, CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(dto.Id, cancellationToken);

        if (category is null)
            return false;

        category.Update(dto.Name, dto.Description);

        _repository.Update(category);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
