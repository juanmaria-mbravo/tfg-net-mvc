using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TfgNetMvc.Application.DTOs.WarehouseLocations;
using TfgNetMvc.Application.UseCases.WarehouseLocations;
using TfgNetMvc.Web.ViewModels.WarehouseLocations;

namespace TfgNetMvc.Web.Controllers;

[Authorize]
public class WarehouseLocationsController : Controller
{
    private readonly GetWarehouseLocations _getWarehouseLocations;
    private readonly GetWarehouseLocationById _getWarehouseLocationById;
    private readonly CreateWarehouseLocation _createWarehouseLocation;
    private readonly UpdateWarehouseLocation _updateWarehouseLocation;
    private readonly DeleteWarehouseLocation _deleteWarehouseLocation;
    private readonly IMapper _mapper;

    public WarehouseLocationsController(
        GetWarehouseLocations getWarehouseLocations,
        GetWarehouseLocationById getWarehouseLocationById,
        CreateWarehouseLocation createWarehouseLocation,
        UpdateWarehouseLocation updateWarehouseLocation,
        DeleteWarehouseLocation deleteWarehouseLocation,
        IMapper mapper)
    {
        _getWarehouseLocations = getWarehouseLocations;
        _getWarehouseLocationById = getWarehouseLocationById;
        _createWarehouseLocation = createWarehouseLocation;
        _updateWarehouseLocation = updateWarehouseLocation;
        _deleteWarehouseLocation = deleteWarehouseLocation;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var locations = await _getWarehouseLocations.ExecuteAsync();
        var viewModels = _mapper.Map<List<WarehouseLocationListViewModel>>(locations);

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var location = await _getWarehouseLocationById.ExecuteAsync(id);

        if (location is null)
            return NotFound();

        var viewModel = _mapper.Map<WarehouseLocationDetailsViewModel>(location);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View(new CreateWarehouseLocationViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateWarehouseLocationViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<CreateWarehouseLocationDto>(viewModel);

        await _createWarehouseLocation.ExecuteAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var location = await _getWarehouseLocationById.ExecuteAsync(id);

        if (location is null)
            return NotFound();

        var viewModel = _mapper.Map<EditWarehouseLocationViewModel>(location);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(EditWarehouseLocationViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<UpdateWarehouseLocationDto>(viewModel);

        var updated = await _updateWarehouseLocation.ExecuteAsync(dto);

        if (!updated)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var location = await _getWarehouseLocationById.ExecuteAsync(id);

        if (location is null)
            return NotFound();

        var viewModel = _mapper.Map<WarehouseLocationDetailsViewModel>(location);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _deleteWarehouseLocation.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
