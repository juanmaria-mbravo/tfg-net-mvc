namespace TfgNetMvc.Application.DTOs.WarehouseLocations;

public class UpdateWarehouseLocationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
