namespace TfgNetMvc.Web.ViewModels.StockMovements;

public class StockMovementViewModel
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string? ItemName { get; set; }
    public string Type { get; set; } = default!;
    public int Quantity { get; set; }
    public int PreviousStock { get; set; }
    public int NewStock { get; set; }
    public string? Reason { get; set; }
    public DateTime MovementDateUtc { get; set; }
    public string? SupplierName { get; set; }
    public string? WarehouseLocationName { get; set; }
}
