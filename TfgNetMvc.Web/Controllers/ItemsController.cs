using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TfgNetMvc.Application.DTOs.Items;
using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Web.ViewModels.Items;

namespace TfgNetMvc.Web.Controllers;

[Authorize]
public class ItemsController : Controller
{
    private readonly GetItems _getItems;
    private readonly GetItemById _getItemById;
    private readonly CreateItem _createItem;
    private readonly UpdateItem _updateItem;
    private readonly DeleteItem _deleteItem;
    private readonly GetCategories _getCategories;
    private readonly IMapper _mapper;

    public ItemsController(
        GetItems getItems,
        GetItemById getItemById,
        CreateItem createItem,
        UpdateItem updateItem,
        DeleteItem deleteItem,
        GetCategories getCategories,
        IMapper mapper)
    {
        _getItems = getItems;
        _getItemById = getItemById;
        _createItem = createItem;
        _updateItem = updateItem;
        _deleteItem = deleteItem;
        _getCategories = getCategories;
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var viewModel = new CreateItemViewModel
        {
            Categories = await BuildCategorySelectListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateItemViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Categories = await BuildCategorySelectListAsync();
            return View(viewModel);
        }

        var dto = _mapper.Map<CreateItemDto>(viewModel);

        await _createItem.ExecuteAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _getItemById.ExecuteAsync(id);

        if (item is null)
            return NotFound();

        var viewModel = _mapper.Map<EditItemViewModel>(item);
        viewModel.Categories = await BuildCategorySelectListAsync();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(EditItemViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Categories = await BuildCategorySelectListAsync();
            return View(viewModel);
        }

        var dto = _mapper.Map<UpdateItemDto>(viewModel);

        var updated = await _updateItem.ExecuteAsync(dto);

        if (!updated)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _deleteItem.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<SelectListItem>> BuildCategorySelectListAsync()
    {
        var categories = await _getCategories.ExecuteAsync();

        return categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        });
    }
}