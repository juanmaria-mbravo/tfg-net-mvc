using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TfgNetMvc.Web.ViewModels.StockMovements;

public class RegisterStockEntryViewModel
{
    [Required]
    public int ItemId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
    public int Quantity { get; set; }

    [MaxLength(500)]
    public string? Reason { get; set; }

    public int? SupplierId { get; set; }

    public int? WarehouseLocationId { get; set; }

    public IEnumerable<SelectListItem> Items { get; set; } = Enumerable.Empty<SelectListItem>();
    public IEnumerable<SelectListItem> Suppliers { get; set; } = Enumerable.Empty<SelectListItem>();
    public IEnumerable<SelectListItem> WarehouseLocations { get; set; } = Enumerable.Empty<SelectListItem>();
}
