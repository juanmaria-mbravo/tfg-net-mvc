using AutoMapper;
using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Suppliers;

public class GetSupplierById
{
    private readonly ISupplierRepository _repository;
    private readonly IMapper _mapper;

    public GetSupplierById(ISupplierRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SupplierDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var supplier = await _repository.GetByIdAsync(id, cancellationToken);
        return supplier is null ? null : _mapper.Map<SupplierDto>(supplier);
    }
}
