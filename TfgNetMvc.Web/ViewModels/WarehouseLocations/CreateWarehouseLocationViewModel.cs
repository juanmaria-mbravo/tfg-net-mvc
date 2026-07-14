using System.ComponentModel.DataAnnotations;

namespace TfgNetMvc.Web.ViewModels.WarehouseLocations;

public class CreateWarehouseLocationViewModel
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string? Description { get; set; }
}
