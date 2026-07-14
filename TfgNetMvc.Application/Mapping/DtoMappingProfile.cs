using AutoMapper;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Application.Mapping;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));
        CreateMap<Category, CategoryDto>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<WarehouseLocation, WarehouseLocationDto>();
        CreateMap<StockMovement, StockMovementDto>()
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item != null ? src.Item.Name : null))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : null))
            .ForMember(dest => dest.WarehouseLocationName, opt => opt.MapFrom(src => src.WarehouseLocation != null ? src.WarehouseLocation.Name : null));
    }
}
