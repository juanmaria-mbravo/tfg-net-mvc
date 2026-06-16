using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Categories;

public class DeleteCategory
{
    private readonly ICategoryRepository _repository;

    public DeleteCategory(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            return false;

        _repository.Delete(category);
        await _repository.SaveChangesAsync();

        return true;
    }
}
