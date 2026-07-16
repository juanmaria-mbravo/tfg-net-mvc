using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TfgNetMvc.Web.ViewModels.Items;

public class EditItemViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

    public int? WarehouseLocationId { get; set; }

    public IEnumerable<SelectListItem> WarehouseLocations { get; set; } = Enumerable.Empty<SelectListItem>();
}
