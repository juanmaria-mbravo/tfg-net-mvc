using TfgNetMvc.Domain.Entities;
using TfgNetMvc.Domain.Enums;

namespace TfgNetMvc.Tests.Domain.Entities;

public class StockMovementTests
{
    [Fact]
    public void Constructor_ValidEntry_SetsProperties()
    {
        var movement = new StockMovement(1, StockMovementType.Entry, 10, 5, 15, "Restock", null, null);

        Assert.Equal(1, movement.ItemId);
        Assert.Equal(StockMovementType.Entry, movement.Type);
        Assert.Equal(10, movement.Quantity);
        Assert.Equal(5, movement.PreviousStock);
        Assert.Equal(15, movement.NewStock);
        Assert.Equal("Restock", movement.Reason);
    }

    [Fact]
    public void Constructor_ZeroQuantity_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            new StockMovement(1, StockMovementType.Entry, 0, 5, 5, null, null, null));
    }

    [Fact]
    public void Constructor_NegativeQuantity_Throws()
    {
        Assert.Throws<ArgumentException>(() =>
            new StockMovement(1, StockMovementType.Exit, -5, 10, 5, null, null, null));
    }

    [Fact]
    public void Constructor_SetsMovementDateUtcToNow()
    {
        var before = DateTime.UtcNow;
        var movement = new StockMovement(1, StockMovementType.Entry, 1, 0, 1, null, null, null);
        var after = DateTime.UtcNow;

        Assert.InRange(movement.MovementDateUtc, before, after);
    }

    [Fact]
    public void Constructor_WithSupplierAndLocation_SetsOptionalFks()
    {
        var movement = new StockMovement(1, StockMovementType.Entry, 5, 0, 5, null, 3, 7);

        Assert.Equal(3, movement.SupplierId);
        Assert.Equal(7, movement.WarehouseLocationId);
    }
}
