namespace TfgNetMvc.Domain.Entities;

public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Stock { get; private set; }
    public int? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public int? WarehouseLocationId { get; private set; }
    public WarehouseLocation? WarehouseLocation { get; private set; }

    private Item() { }

    public Item(string name, string? description, int stock, int? categoryId = null, int? warehouseLocationId = null)
    {
        SetName(name);
        SetStock(stock);

        Description = description;
        CategoryId = categoryId;
        WarehouseLocationId = warehouseLocationId;
    }

    public void Update(string name, string? description, int? categoryId = null, int? warehouseLocationId = null)
    {
        SetName(name);

        Description = description;
        CategoryId = categoryId;
        WarehouseLocationId = warehouseLocationId;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Stock += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (Stock - quantity < 0)
            throw new InvalidOperationException("Not enough stock.");

        Stock -= quantity;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        Name = name.Trim();
    }

    private void SetStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative.");

        Stock = stock;
    }
}