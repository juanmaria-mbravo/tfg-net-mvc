using AutoMapper;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Web.ViewModels.Categories;
using TfgNetMvc.Web.ViewModels.Items;
using TfgNetMvc.Web.ViewModels.StockMovements;
using TfgNetMvc.Web.ViewModels.Suppliers;
using TfgNetMvc.Web.ViewModels.WarehouseLocations;

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

        CreateMap<SupplierDto, SupplierListViewModel>();
        CreateMap<SupplierDto, SupplierDetailsViewModel>();
        CreateMap<SupplierDto, EditSupplierViewModel>();

        CreateMap<CreateSupplierViewModel, CreateSupplierDto>();
        CreateMap<EditSupplierViewModel, UpdateSupplierDto>();

        CreateMap<WarehouseLocationDto, WarehouseLocationListViewModel>();
        CreateMap<WarehouseLocationDto, WarehouseLocationDetailsViewModel>();
        CreateMap<WarehouseLocationDto, EditWarehouseLocationViewModel>();

        CreateMap<CreateWarehouseLocationViewModel, CreateWarehouseLocationDto>();
        CreateMap<EditWarehouseLocationViewModel, UpdateWarehouseLocationDto>();

        CreateMap<StockMovementDto, StockMovementViewModel>();

        CreateMap<RegisterStockEntryViewModel, RegisterStockEntryDto>();
        CreateMap<RegisterStockExitViewModel, RegisterStockExitDto>();
    }
}
