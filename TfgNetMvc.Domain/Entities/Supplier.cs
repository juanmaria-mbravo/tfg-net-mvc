namespace TfgNetMvc.Domain.Entities;

public class Supplier
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? ContactEmail { get; private set; }
    public string? Phone { get; private set; }
    public string? Notes { get; private set; }

    private Supplier() { }

    public Supplier(string name, string? contactEmail, string? phone, string? notes)
    {
        SetName(name);
        ContactEmail = contactEmail;
        Phone = phone;
        Notes = notes;
    }

    public void Update(string name, string? contactEmail, string? phone, string? notes)
    {
        SetName(name);
        ContactEmail = contactEmail;
        Phone = phone;
        Notes = notes;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        Name = name.Trim();
    }
}
