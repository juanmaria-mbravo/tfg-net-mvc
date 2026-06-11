using System.ComponentModel.DataAnnotations;

namespace TfgNetMvc.Web.ViewModels.Items;

public class CreateItemViewModel
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
