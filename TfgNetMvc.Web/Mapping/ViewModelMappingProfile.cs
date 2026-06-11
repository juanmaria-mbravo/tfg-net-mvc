using AutoMapper;
using TfgNetMvc.Application.DTOs.Items;
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
    }
}
