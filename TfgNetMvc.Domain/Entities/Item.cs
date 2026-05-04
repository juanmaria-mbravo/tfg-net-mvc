namespace TfgNetMvc.Domain.Entities;

public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Stock { get; private set; }

    private Item() { }

    public Item(string name, string? description, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (stock < 0)
            throw new ArgumentException("Stock cannot be negative.");

        Name = name.Trim();
        Description = description;
        Stock = stock;
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
}
