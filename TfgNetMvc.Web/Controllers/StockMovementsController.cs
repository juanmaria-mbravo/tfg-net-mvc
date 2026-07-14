using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TfgNetMvc.Application.DTOs.StockMovements;
using TfgNetMvc.Application.UseCases.Items;
using TfgNetMvc.Application.UseCases.StockMovements;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Web.ViewModels.StockMovements;

namespace TfgNetMvc.Web.Controllers;

[Authorize]
public class StockMovementsController : Controller
{
    private readonly GetStockMovements _getStockMovements;
    private readonly GetMovementsByItem _getMovementsByItem;
    private readonly RegisterStockEntry _registerStockEntry;
    private readonly RegisterStockExit _registerStockExit;
    private readonly GetItems _getItems;
    private readonly GetSuppliers _getSuppliers;
    private readonly GetWarehouseLocations _getWarehouseLocations;
    private readonly IMapper _mapper;

    public StockMovementsController(
        GetStockMovements getStockMovements,
        GetMovementsByItem getMovementsByItem,
        RegisterStockEntry registerStockEntry,
        RegisterStockExit registerStockExit,
        GetItems getItems,
        GetSuppliers getSuppliers,
        GetWarehouseLocations getWarehouseLocations,
        IMapper mapper)
    {
        _getStockMovements = getStockMovements;
        _getMovementsByItem = getMovementsByItem;
        _registerStockEntry = registerStockEntry;
        _registerStockExit = registerStockExit;
        _getItems = getItems;
        _getSuppliers = getSuppliers;
        _getWarehouseLocations = getWarehouseLocations;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var movements = await _getStockMovements.ExecuteAsync();
        var viewModels = _mapper.Map<List<StockMovementViewModel>>(movements);

        return View(viewModels);
    }

    public async Task<IActionResult> ByItem(int itemId)
    {
        var movements = await _getMovementsByItem.ExecuteAsync(itemId);
        var viewModels = _mapper.Map<List<StockMovementViewModel>>(movements);

        ViewBag.ItemId = itemId;
        if (viewModels.Count > 0)
            ViewBag.ItemName = viewModels[0].ItemName;

        return View(viewModels);
    }

    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> RegisterEntry()
    {
        var viewModel = new RegisterStockEntryViewModel
        {
            Items = await BuildItemSelectListAsync(),
            Suppliers = await BuildSupplierSelectListAsync(),
            WarehouseLocations = await BuildLocationSelectListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> RegisterEntry(RegisterStockEntryViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Items = await BuildItemSelectListAsync();
            viewModel.Suppliers = await BuildSupplierSelectListAsync();
            viewModel.WarehouseLocations = await BuildLocationSelectListAsync();
            return View(viewModel);
        }

        var dto = _mapper.Map<RegisterStockEntryDto>(viewModel);

        try
        {
            await _registerStockEntry.ExecuteAsync(dto);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            viewModel.Items = await BuildItemSelectListAsync();
            viewModel.Suppliers = await BuildSupplierSelectListAsync();
            viewModel.WarehouseLocations = await BuildLocationSelectListAsync();
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> RegisterExit()
    {
        var viewModel = new RegisterStockExitViewModel
        {
            Items = await BuildItemSelectListAsync(),
            WarehouseLocations = await BuildLocationSelectListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> RegisterExit(RegisterStockExitViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Items = await BuildItemSelectListAsync();
            viewModel.WarehouseLocations = await BuildLocationSelectListAsync();
            return View(viewModel);
        }

        var dto = _mapper.Map<RegisterStockExitDto>(viewModel);

        try
        {
            await _registerStockExit.ExecuteAsync(dto);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            viewModel.Items = await BuildItemSelectListAsync();
            viewModel.WarehouseLocations = await BuildLocationSelectListAsync();
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<IEnumerable<SelectListItem>> BuildItemSelectListAsync()
    {
        var items = await _getItems.ExecuteAsync();
        return items.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name });
    }

    private async Task<IEnumerable<SelectListItem>> BuildSupplierSelectListAsync()
    {
        var suppliers = await _getSuppliers.ExecuteAsync();
        return suppliers.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
    }

    private async Task<IEnumerable<SelectListItem>> BuildLocationSelectListAsync()
    {
        var locations = await _getWarehouseLocations.ExecuteAsync();
        return locations.Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.Name });
    }
}
