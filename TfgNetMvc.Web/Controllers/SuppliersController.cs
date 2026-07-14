using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TfgNetMvc.Application.DTOs.Suppliers;
using TfgNetMvc.Application.UseCases.Suppliers;
using TfgNetMvc.Web.ViewModels.Suppliers;

namespace TfgNetMvc.Web.Controllers;

[Authorize]
public class SuppliersController : Controller
{
    private readonly GetSuppliers _getSuppliers;
    private readonly GetSupplierById _getSupplierById;
    private readonly CreateSupplier _createSupplier;
    private readonly UpdateSupplier _updateSupplier;
    private readonly DeleteSupplier _deleteSupplier;
    private readonly IMapper _mapper;

    public SuppliersController(
        GetSuppliers getSuppliers,
        GetSupplierById getSupplierById,
        CreateSupplier createSupplier,
        UpdateSupplier updateSupplier,
        DeleteSupplier deleteSupplier,
        IMapper mapper)
    {
        _getSuppliers = getSuppliers;
        _getSupplierById = getSupplierById;
        _createSupplier = createSupplier;
        _updateSupplier = updateSupplier;
        _deleteSupplier = deleteSupplier;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var suppliers = await _getSuppliers.ExecuteAsync();
        var viewModels = _mapper.Map<List<SupplierListViewModel>>(suppliers);

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var supplier = await _getSupplierById.ExecuteAsync(id);

        if (supplier is null)
            return NotFound();

        var viewModel = _mapper.Map<SupplierDetailsViewModel>(supplier);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View(new CreateSupplierViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateSupplierViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<CreateSupplierDto>(viewModel);

        await _createSupplier.ExecuteAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var supplier = await _getSupplierById.ExecuteAsync(id);

        if (supplier is null)
            return NotFound();

        var viewModel = _mapper.Map<EditSupplierViewModel>(supplier);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(EditSupplierViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<UpdateSupplierDto>(viewModel);

        var updated = await _updateSupplier.ExecuteAsync(dto);

        if (!updated)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var supplier = await _getSupplierById.ExecuteAsync(id);

        if (supplier is null)
            return NotFound();

        var viewModel = _mapper.Map<SupplierDetailsViewModel>(supplier);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _deleteSupplier.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
