namespace TfgNetMvc.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    private Category() { }

    public Category(string name, string? description)
    {
        SetName(name);
        Description = description;
    }

    public void Update(string name, string? description)
    {
        SetName(name);
        Description = description;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        Name = name.Trim();
    }
}
