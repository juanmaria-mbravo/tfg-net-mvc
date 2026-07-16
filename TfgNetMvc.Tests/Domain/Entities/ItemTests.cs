using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Domain.Entities;

public class ItemTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateItem()
    {
        var item = new Item("Laptop", "Work laptop", 10);

        Assert.Equal("Laptop", item.Name);
        Assert.Equal("Work laptop", item.Description);
        Assert.Equal(10, item.Stock);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Item("", "Description", 10));
    }

    [Fact]
    public void Constructor_WithNegativeStock_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Item("Laptop", "Description", -1));
    }

    [Fact]
    public void AddStock_WithPositiveQuantity_ShouldIncreaseStock()
    {
        var item = new Item("Laptop", "Description", 10);

        item.AddStock(5);

        Assert.Equal(15, item.Stock);
    }

    [Fact]
    public void AddStock_WithZeroQuantity_ShouldThrowArgumentException()
    {
        var item = new Item("Laptop", "Description", 10);

        Assert.Throws<ArgumentException>(() =>
            item.AddStock(0));
    }

    [Fact]
    public void RemoveStock_WithValidQuantity_ShouldDecreaseStock()
    {
        var item = new Item("Laptop", "Description", 10);

        item.RemoveStock(4);

        Assert.Equal(6, item.Stock);
    }

    [Fact]
    public void RemoveStock_WithQuantityGreaterThanStock_ShouldThrowInvalidOperationException()
    {
        var item = new Item("Laptop", "Description", 10);

        Assert.Throws<InvalidOperationException>(() =>
            item.RemoveStock(11));
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateItem()
    {
        var item = new Item("Laptop", "Description", 10);

        item.Update("Monitor", "Updated description");

        Assert.Equal("Monitor", item.Name);
        Assert.Equal("Updated description", item.Description);
    }

    [Fact]
    public void Update_StockRemainsUnchanged()
    {
        var item = new Item("Laptop", "Description", 10);

        item.Update("Monitor", "Updated description");

        Assert.Equal(10, item.Stock);
    }
}
