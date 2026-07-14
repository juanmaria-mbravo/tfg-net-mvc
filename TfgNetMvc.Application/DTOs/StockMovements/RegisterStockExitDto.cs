namespace TfgNetMvc.Application.DTOs.StockMovements;

public class RegisterStockExitDto
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public int? WarehouseLocationId { get; set; }
}
