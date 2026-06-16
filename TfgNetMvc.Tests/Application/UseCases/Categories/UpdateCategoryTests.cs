using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Categories;

public class UpdateCategoryTests
{
    [Fact]
    public async Task ExecuteAsync_WhenCategoryExists_UpdatesCategory()
    {
        var repository = new FakeCategoryRepository();
        var existing = new Category("Electronics", "Old description");

        await repository.AddAsync(existing);
        await repository.SaveChangesAsync();

        var useCase = new UpdateCategory(repository);

        var dto = new UpdateCategoryDto
        {
            Id = existing.Id,
            Name = "Computers",
            Description = "New description"
        };

        var result = await useCase.ExecuteAsync(dto);

        var updated = await repository.GetByIdAsync(existing.Id);

        Assert.True(result);
        Assert.NotNull(updated);
        Assert.Equal("Computers", updated.Name);
        Assert.Equal("New description", updated.Description);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCategoryDoesNotExist_ReturnsFalse()
    {
        var repository = new FakeCategoryRepository();
        var useCase = new UpdateCategory(repository);

        var dto = new UpdateCategoryDto
        {
            Id = 999,
            Name = "Computers",
            Description = "Description"
        };

        var result = await useCase.ExecuteAsync(dto);

        Assert.False(result);
    }
}
