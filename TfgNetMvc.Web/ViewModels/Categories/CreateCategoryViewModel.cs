using System.ComponentModel.DataAnnotations;

namespace TfgNetMvc.Web.ViewModels.Categories;

public class CreateCategoryViewModel
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}
