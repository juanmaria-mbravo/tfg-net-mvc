using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Suppliers;

public class DeleteSupplier
{
    private readonly ISupplierRepository _repository;

    public DeleteSupplier(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var supplier = await _repository.GetByIdAsync(id, cancellationToken);

        if (supplier is null)
            return false;

        _repository.Delete(supplier);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
