using Microsoft.EntityFrameworkCore;
using TfgNetMvc.Infrastructure.Persistence;
using TfgNetMvc.Application.Interfaces.Repositories;
using TfgNetMvc.Infrastructure.Repositories;
using TfgNetMvc.Application.UseCases.Items;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<CreateItem>();
builder.Services.AddScoped<GetItems>();
builder.Services.AddScoped<GetItemById>();
builder.Services.AddScoped<UpdateItem>();
builder.Services.AddScoped<DeleteItem>();

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

app.Run();

public partial class Program { }
