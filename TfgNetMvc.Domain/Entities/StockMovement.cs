using TfgNetMvc.Domain.Enums;

namespace TfgNetMvc.Domain.Entities;

public class StockMovement
{
    public int Id { get; private set; }
    public int ItemId { get; private set; }
    public Item Item { get; private set; } = default!;
    public StockMovementType Type { get; private set; }
    public int Quantity { get; private set; }
    public int PreviousStock { get; private set; }
    public int NewStock { get; private set; }
    public string? Reason { get; private set; }
    public DateTime MovementDateUtc { get; private set; }
    public int? SupplierId { get; private set; }
    public Supplier? Supplier { get; private set; }
    public int? WarehouseLocationId { get; private set; }
    public WarehouseLocation? WarehouseLocation { get; private set; }

    private StockMovement() { }

    public StockMovement(
        int itemId,
        StockMovementType type,
        int quantity,
        int previousStock,
        int newStock,
        string? reason,
        int? supplierId,
        int? warehouseLocationId)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        ItemId = itemId;
        Type = type;
        Quantity = quantity;
        PreviousStock = previousStock;
        NewStock = newStock;
        Reason = reason;
        MovementDateUtc = DateTime.UtcNow;
        SupplierId = supplierId;
        WarehouseLocationId = warehouseLocationId;
    }
}
