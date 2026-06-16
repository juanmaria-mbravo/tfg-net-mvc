using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Application.Fakes;

public class FakeCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = new();
    private int _nextId = 1;

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_categories.ToList());
    }

    public Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = _categories.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(category);
    }

    public Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        SetId(category, _nextId++);
        _categories.Add(category);

        return Task.CompletedTask;
    }

    public void Update(Category category)
    {
    }

    public void Delete(Category category)
    {
        _categories.Remove(category);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    private static void SetId(Category category, int id)
    {
        var property = typeof(Category).GetProperty(nameof(Category.Id));

        if (property is null)
            throw new InvalidOperationException("Category Id property was not found.");

        property.SetValue(category, id);
    }
}
