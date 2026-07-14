namespace TfgNetMvc.Application.DTOs.Suppliers;

public class CreateSupplierDto
{
    public string Name { get; set; } = string.Empty;
    public string? ContactEmail { get; set; }
    public string? Phone { get; set; }
    public string? Notes { get; set; }
}
