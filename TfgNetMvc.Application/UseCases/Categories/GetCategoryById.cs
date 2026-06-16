using AutoMapper;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.Interfaces.Repositories;

namespace TfgNetMvc.Application.UseCases.Categories;

public class GetCategoryById
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryById(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken);

        if (category is null)
            return null;

        return _mapper.Map<CategoryDto>(category);
    }
}
