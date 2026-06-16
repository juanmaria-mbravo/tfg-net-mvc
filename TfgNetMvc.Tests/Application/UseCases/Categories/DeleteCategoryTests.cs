using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Categories;

public class DeleteCategoryTests
{
    [Fact]
    public async Task ExecuteAsync_WithExistingCategory_ShouldDeleteCategory()
    {
        var repository = new FakeCategoryRepository();
        var category = new Category("Electronics", "Electronic devices");
        await repository.AddAsync(category);

        var useCase = new DeleteCategory(repository);

        var result = await useCase.ExecuteAsync(category.Id);

        var deleted = await repository.GetByIdAsync(category.Id);

        Assert.True(result);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistingCategory_ShouldReturnFalse()
    {
        var repository = new FakeCategoryRepository();
        var useCase = new DeleteCategory(repository);

        var result = await useCase.ExecuteAsync(999);

        Assert.False(result);
    }
}
