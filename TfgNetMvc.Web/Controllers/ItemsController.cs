using Microsoft.AspNetCore.Mvc;
using TfgNetMvc.Application.UseCases.Items;

namespace TfgNetMvc.Web.Controllers;

public class ItemsController : Controller
{
    private readonly GetItems _getItems;
    private readonly GetItemById _getItemById;
    private readonly CreateItem _createItem;
    private readonly UpdateItem _updateItem;
    private readonly DeleteItem _deleteItem;

    public ItemsController(
        GetItems getItems,
        GetItemById getItemById,
        CreateItem createItem,
        UpdateItem updateItem,
        DeleteItem deleteItem)
    {
        _getItems = getItems;
        _getItemById = getItemById;
        _createItem = createItem;
        _updateItem = updateItem;
        _deleteItem = deleteItem;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _getItems.ExecuteAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        return View(item);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name, string? description, int stock)
    {
        if (!ModelState.IsValid)
            return View();

        await _createItem.ExecuteAsync(name, description, stock);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, string name, string? description, int stock)
    {
        if (!ModelState.IsValid)
            return View();

        var updated = await _updateItem.ExecuteAsync(id, name, description, stock);

        if (!updated)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _deleteItem.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
