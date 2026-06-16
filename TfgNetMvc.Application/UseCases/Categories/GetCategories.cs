using AutoMapper;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Categories;

public class GetCategories
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategories(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}
