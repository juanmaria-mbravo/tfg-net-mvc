using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Domain.Entities;

public class SupplierTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateSupplier()
    {
        var supplier = new Supplier("Acme Corp", "acme@example.com", "600123456", "Main supplier");

        Assert.Equal("Acme Corp", supplier.Name);
        Assert.Equal("acme@example.com", supplier.ContactEmail);
        Assert.Equal("600123456", supplier.Phone);
        Assert.Equal("Main supplier", supplier.Notes);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Supplier("", null, null, null));
    }

    [Fact]
    public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Supplier("   ", null, null, null));
    }

    [Fact]
    public void Constructor_NameShouldBeTrimmed()
    {
        var supplier = new Supplier("  Acme Corp  ", null, null, null);

        Assert.Equal("Acme Corp", supplier.Name);
    }

    [Fact]
    public void Constructor_WithNullOptionalFields_ShouldCreateSupplier()
    {
        var supplier = new Supplier("Acme Corp", null, null, null);

        Assert.Equal("Acme Corp", supplier.Name);
        Assert.Null(supplier.ContactEmail);
        Assert.Null(supplier.Phone);
        Assert.Null(supplier.Notes);
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateSupplier()
    {
        var supplier = new Supplier("Acme Corp", "old@example.com", "600000000", "Old notes");

        supplier.Update("New Supplier", "new@example.com", "611111111", "New notes");

        Assert.Equal("New Supplier", supplier.Name);
        Assert.Equal("new@example.com", supplier.ContactEmail);
        Assert.Equal("611111111", supplier.Phone);
        Assert.Equal("New notes", supplier.Notes);
    }

    [Fact]
    public void Update_WithEmptyName_ShouldThrowArgumentException()
    {
        var supplier = new Supplier("Acme Corp", null, null, null);

        Assert.Throws<ArgumentException>(() =>
            supplier.Update("", null, null, null));
    }

    [Fact]
    public void Update_NameShouldBeTrimmed()
    {
        var supplier = new Supplier("Acme Corp", null, null, null);

        supplier.Update("  New Name  ", null, null, null);

        Assert.Equal("New Name", supplier.Name);
    }
}
