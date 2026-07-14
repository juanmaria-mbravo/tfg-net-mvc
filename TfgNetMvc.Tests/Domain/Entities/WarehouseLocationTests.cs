using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Domain.Entities;

public class WarehouseLocationTests
{
    [Fact]
    public void Constructor_ValidName_SetsName()
    {
        var location = new WarehouseLocation("Shelf A", null);
        Assert.Equal("Shelf A", location.Name);
    }

    [Fact]
    public void Constructor_TrimsName()
    {
        var location = new WarehouseLocation("  Shelf B  ", null);
        Assert.Equal("Shelf B", location.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_Throws(string? name)
    {
        Assert.Throws<ArgumentException>(() => new WarehouseLocation(name!, null));
    }

    [Fact]
    public void Constructor_WithDescription_SetsDescription()
    {
        var location = new WarehouseLocation("Zone 1", "Main storage area");
        Assert.Equal("Main storage area", location.Description);
    }

    [Fact]
    public void Update_ChangesNameAndDescription()
    {
        var location = new WarehouseLocation("Old Name", null);
        location.Update("New Name", "New desc");
        Assert.Equal("New Name", location.Name);
        Assert.Equal("New desc", location.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Update_InvalidName_Throws(string? name)
    {
        var location = new WarehouseLocation("Shelf A", null);
        Assert.Throws<ArgumentException>(() => location.Update(name!, null));
    }
}
