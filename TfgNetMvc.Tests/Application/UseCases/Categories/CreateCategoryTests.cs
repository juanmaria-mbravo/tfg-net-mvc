using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Categories;

public class CreateCategoryTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidData_CreatesCategory()
    {
        var repository = new FakeCategoryRepository();
        var useCase = new CreateCategory(repository);

        var dto = new CreateCategoryDto
        {
            Name = "Electronics",
            Description = "Electronic devices"
        };

        var id = await useCase.ExecuteAsync(dto);

        var created = await repository.GetByIdAsync(id);

        Assert.NotNull(created);
        Assert.Equal("Electronics", created.Name);
        Assert.Equal("Electronic devices", created.Description);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyName_ThrowsArgumentException()
    {
        var repository = new FakeCategoryRepository();
        var useCase = new CreateCategory(repository);

        var dto = new CreateCategoryDto
        {
            Name = "",
            Description = "Invalid category"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
    }
}
