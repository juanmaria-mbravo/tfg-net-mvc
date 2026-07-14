using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Application.Mapping;
using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Application.UseCases.StockMovements;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Infrastructure.Identity;
using TfgNetMvc.Infrastructure.Persistence;
using TfgNetMvc.Infrastructure.Repositories;
using TfgNetMvc.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.IsRelational())
    {
        db.Database.Migrate();
        await DataSeeder.SeedAsync(scope.ServiceProvider);
    }
}

app.Run();

public partial class Program { }
