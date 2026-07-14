using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Suppliers;

public class UpdateSupplier
{
    private readonly ISupplierRepository _repository;

    public UpdateSupplier(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(UpdateSupplierDto dto, CancellationToken cancellationToken = default)
    {
        var supplier = await _repository.GetByIdAsync(dto.Id, cancellationToken);

        if (supplier is null)
            return false;

        supplier.Update(dto.Name, dto.ContactEmail, dto.Phone, dto.Notes);

        _repository.Update(supplier);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
