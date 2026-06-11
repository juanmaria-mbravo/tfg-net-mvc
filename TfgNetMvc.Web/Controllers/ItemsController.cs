using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Web.ViewModels.Items;

namespace TfgNetMvc.Web.Controllers;

public class ItemsController : Controller
{
    private readonly GetItems _getItems;
    private readonly GetItemById _getItemById;
    private readonly CreateItem _createItem;
    private readonly UpdateItem _updateItem;
    private readonly DeleteItem _deleteItem;
    private readonly IMapper _mapper;

    public ItemsController(
        GetItems getItems,
        GetItemById getItemById,
        CreateItem createItem,
        UpdateItem updateItem,
        DeleteItem deleteItem,
        IMapper mapper)
    {
        _getItems = getItems;
        _getItemById = getItemById;
        _createItem = createItem;
        _updateItem = updateItem;
        _deleteItem = deleteItem;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var items = await _getItems.ExecuteAsync();
        var viewModels = _mapper.Map<List<ItemListViewModel>>(items);

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        var viewModel = _mapper.Map<ItemDetailsViewModel>(item);

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View(new CreateItemViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateItemViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<CreateItemDto>(viewModel);

        await _createItem.ExecuteAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        var viewModel = _mapper.Map<EditItemViewModel>(item);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditItemViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<UpdateItemDto>(viewModel);

        var updated = await _updateItem.ExecuteAsync(dto);

        if (!updated)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        var viewModel = _mapper.Map<ItemDetailsViewModel>(item);

        return View(viewModel);
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