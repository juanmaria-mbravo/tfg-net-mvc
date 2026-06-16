using System.ComponentModel.DataAnnotations;

namespace TfgNetMvc.Web.ViewModels.Categories;

public class EditCategoryViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }
}
