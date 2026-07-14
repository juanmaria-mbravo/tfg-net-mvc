using AutoMapper;
using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Suppliers;

public class GetSuppliers
{
    private readonly ISupplierRepository _repository;
    private readonly IMapper _mapper;

    public GetSuppliers(ISupplierRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<SupplierDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var suppliers = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<SupplierDto>>(suppliers);
    }
}
