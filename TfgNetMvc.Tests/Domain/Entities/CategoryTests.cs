using TfgNetMvc.Domain.Entities;

namespace TfgNetMvc.Tests.Domain.Entities;

public class CategoryTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateCategory()
    {
        var category = new Category("Electronics", "Electronic devices");

        Assert.Equal("Electronics", category.Name);
        Assert.Equal("Electronic devices", category.Description);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Category("", "Description"));
    }

    [Fact]
    public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Category("   ", "Description"));
    }

    [Fact]
    public void Constructor_NameShouldBeTrimmed()
    {
        var category = new Category("  Electronics  ", null);

        Assert.Equal("Electronics", category.Name);
    }

    [Fact]
    public void Constructor_WithNullDescription_ShouldCreateCategory()
    {
        var category = new Category("Electronics", null);

        Assert.Equal("Electronics", category.Name);
        Assert.Null(category.Description);
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateCategory()
    {
        var category = new Category("Electronics", "Old description");

        category.Update("Computers", "New description");

        Assert.Equal("Computers", category.Name);
        Assert.Equal("New description", category.Description);
    }

    [Fact]
    public void Update_WithEmptyName_ShouldThrowArgumentException()
    {
        var category = new Category("Electronics", "Description");

        Assert.Throws<ArgumentException>(() =>
            category.Update("", "Description"));
    }

    [Fact]
    public void Update_NameShouldBeTrimmed()
    {
        var category = new Category("Electronics", null);

        category.Update("  Computers  ", null);

        Assert.Equal("Computers", category.Name);
    }
}
