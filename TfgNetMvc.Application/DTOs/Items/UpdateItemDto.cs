namespace TfgNetMvc.Application.DTOs.Items;

public class UpdateItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
}
