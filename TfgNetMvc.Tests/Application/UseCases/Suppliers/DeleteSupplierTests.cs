using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Suppliers;

public class DeleteSupplierTests
{
    [Fact]
    public async Task ExecuteAsync_WithExistingId_DeletesSupplier()
    {
        var repository = new FakeSupplierRepository();
        var createUseCase = new CreateSupplier(repository);
        var deleteUseCase = new DeleteSupplier(repository);

        var id = await createUseCase.ExecuteAsync(new CreateSupplierDto { Name = "Acme Corp" });

        var result = await deleteUseCase.ExecuteAsync(id);
        var deleted = await repository.GetByIdAsync(id);

        Assert.True(result);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistentId_ReturnsFalse()
    {
        var repository = new FakeSupplierRepository();
        var useCase = new DeleteSupplier(repository);

        var result = await useCase.ExecuteAsync(99);

        Assert.False(result);
    }
}
