using System.ComponentModel.DataAnnotations;

namespace TfgNetMvc.Web.ViewModels.Suppliers;

public class EditSupplierViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    [EmailAddress]
    public string? ContactEmail { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}
