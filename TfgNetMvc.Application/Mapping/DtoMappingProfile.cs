using AutoMapper;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Mapping;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        CreateMap<Category, CategoryDto>();
    }
}
