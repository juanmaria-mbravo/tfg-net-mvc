namespace TfgNetMvc.Web.ViewModels.Items;

public class ItemDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public string? CategoryName { get; set; }
    public string? WarehouseLocationName { get; set; }
}
