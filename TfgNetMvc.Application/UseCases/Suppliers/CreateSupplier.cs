using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.UseCases.Suppliers;

public class CreateSupplier
{
    private readonly ISupplierRepository _repository;

    public CreateSupplier(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> ExecuteAsync(CreateSupplierDto dto, CancellationToken cancellationToken = default)
    {
        var supplier = new Supplier(dto.Name, dto.ContactEmail, dto.Phone, dto.Notes);

        await _repository.AddAsync(supplier, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return supplier.Id;
    }
}
