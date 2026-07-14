using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Suppliers;

public class UpdateSupplierTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidData_UpdatesSupplier()
    {
        var repository = new FakeSupplierRepository();
        var createUseCase = new CreateSupplier(repository);
        var updateUseCase = new UpdateSupplier(repository);

        var id = await createUseCase.ExecuteAsync(new CreateSupplierDto
        {
            Name = "Acme Corp",
            ContactEmail = "old@example.com"
        });

        var result = await updateUseCase.ExecuteAsync(new UpdateSupplierDto
        {
            Id = id,
            Name = "New Supplier",
            ContactEmail = "new@example.com"
        });

        var updated = await repository.GetByIdAsync(id);

        Assert.True(result);
        Assert.Equal("New Supplier", updated!.Name);
        Assert.Equal("new@example.com", updated.ContactEmail);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistentId_ReturnsFalse()
    {
        var repository = new FakeSupplierRepository();
        var useCase = new UpdateSupplier(repository);

        var result = await useCase.ExecuteAsync(new UpdateSupplierDto { Id = 99, Name = "X" });

        Assert.False(result);
    }
}
