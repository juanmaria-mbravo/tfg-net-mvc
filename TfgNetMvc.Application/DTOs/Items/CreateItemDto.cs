namespace TfgNetMvc.Application.DTOs.Items;

public class CreateItemDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public int? WarehouseLocationId { get; set; }
}
