using System.ComponentModel.DataAnnotations;

namespace TfgNetMvc.Web.ViewModels.WarehouseLocations;

public class EditWarehouseLocationViewModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string? Description { get; set; }
}
