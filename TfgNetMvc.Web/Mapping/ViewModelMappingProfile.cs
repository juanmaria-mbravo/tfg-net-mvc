using AutoMapper;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Web.ViewModels.Categories;
using TfgNetMvc.Web.ViewModels.Items;

namespace TfgNetMvc.Web.Mapping;

public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<ItemDto, ItemListViewModel>();
        CreateMap<ItemDto, ItemDetailsViewModel>();
        CreateMap<ItemDto, EditItemViewModel>();

        CreateMap<CreateItemViewModel, CreateItemDto>();
        CreateMap<EditItemViewModel, UpdateItemDto>();

        CreateMap<CategoryDto, CategoryListViewModel>();
        CreateMap<CategoryDto, CategoryDetailsViewModel>();
        CreateMap<CategoryDto, EditCategoryViewModel>();

        CreateMap<CreateCategoryViewModel, CreateCategoryDto>();
        CreateMap<EditCategoryViewModel, UpdateCategoryDto>();
    }
}
