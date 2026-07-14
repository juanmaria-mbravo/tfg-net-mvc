using Microsoft.EntityFrameworkCore;
using TfgNetMvc.Infrastructure.Persistence;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Infrastructure.Repositories;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Application.UseCases.StockMovements;
using TfgNetMvc.Application.Mapping;
using TfgNetMvc.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DtoMappingProfile>();
    cfg.AddProfile<ViewModelMappingProfile>();
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<CreateItem>();
builder.Services.AddScoped<GetItems>();
builder.Services.AddScoped<GetItemById>();
builder.Services.AddScoped<UpdateItem>();
builder.Services.AddScoped<DeleteItem>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CreateCategory>();
builder.Services.AddScoped<GetCategories>();
builder.Services.AddScoped<GetCategoryById>();
builder.Services.AddScoped<UpdateCategory>();
builder.Services.AddScoped<DeleteCategory>();

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<CreateSupplier>();
builder.Services.AddScoped<GetSuppliers>();
builder.Services.AddScoped<GetSupplierById>();
builder.Services.AddScoped<UpdateSupplier>();
builder.Services.AddScoped<DeleteSupplier>();

builder.Services.AddScoped<IWarehouseLocationRepository, WarehouseLocationRepository>();
builder.Services.AddScoped<CreateWarehouseLocation>();
builder.Services.AddScoped<GetWarehouseLocations>();
builder.Services.AddScoped<GetWarehouseLocationById>();
builder.Services.AddScoped<UpdateWarehouseLocation>();
builder.Services.AddScoped<DeleteWarehouseLocation>();

builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<RegisterStockEntry>();
builder.Services.AddScoped<RegisterStockExit>();
builder.Services.AddScoped<GetStockMovements>();
builder.Services.AddScoped<GetMovementsByItem>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.IsRelational())
        db.Database.Migrate();
}

app.Run();

public partial class Program { }
