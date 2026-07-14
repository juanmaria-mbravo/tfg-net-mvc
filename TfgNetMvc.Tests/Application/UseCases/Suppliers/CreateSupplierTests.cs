using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Tests.Application.Fakes;

namespace TfgNetMvc.Tests.Application.UseCases.Suppliers;

public class CreateSupplierTests
{
    [Fact]
    public async Task ExecuteAsync_WithValidData_CreatesSupplier()
    {
        var repository = new FakeSupplierRepository();
        var useCase = new CreateSupplier(repository);

        var dto = new CreateSupplierDto
        {
            Name = "Acme Corp",
            ContactEmail = "acme@example.com",
            Phone = "600123456",
            Notes = "Main supplier"
        };

        var id = await useCase.ExecuteAsync(dto);
        var created = await repository.GetByIdAsync(id);

        Assert.NotNull(created);
        Assert.Equal("Acme Corp", created.Name);
        Assert.Equal("acme@example.com", created.ContactEmail);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyName_ThrowsArgumentException()
    {
        var repository = new FakeSupplierRepository();
        var useCase = new CreateSupplier(repository);

        var dto = new CreateSupplierDto { Name = "" };

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
    }
}
