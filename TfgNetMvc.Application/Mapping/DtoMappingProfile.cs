using AutoMapper;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Mapping;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Item, ItemDto>();
    }
}
