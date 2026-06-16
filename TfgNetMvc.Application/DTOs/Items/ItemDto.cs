namespace TfgNetMvc.Application.DTOs.Items;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
