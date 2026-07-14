using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TfgNetMvc.Application.DTOs.Categories;
using TfgNetMvc.Application.UseCases.Categories;
using TfgNetMvc.Web.ViewModels.Categories;

namespace TfgNetMvc.Web.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly GetCategories _getCategories;
    private readonly GetCategoryById _getCategoryById;
    private readonly CreateCategory _createCategory;
    private readonly UpdateCategory _updateCategory;
    private readonly DeleteCategory _deleteCategory;
    private readonly IMapper _mapper;

    public CategoriesController(
        GetCategories getCategories,
        GetCategoryById getCategoryById,
        CreateCategory createCategory,
        UpdateCategory updateCategory,
        DeleteCategory deleteCategory,
        IMapper mapper)
    {
        _getCategories = getCategories;
        _getCategoryById = getCategoryById;
        _createCategory = createCategory;
        _updateCategory = updateCategory;
        _deleteCategory = deleteCategory;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _getCategories.ExecuteAsync();
        var viewModels = _mapper.Map<List<CategoryListViewModel>>(categories);

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await _getCategoryById.ExecuteAsync(id);

        if (category is null)
            return NotFound();

        var viewModel = _mapper.Map<CategoryDetailsViewModel>(category);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View(new CreateCategoryViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateCategoryViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<CreateCategoryDto>(viewModel);

        await _createCategory.ExecuteAsync(dto);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _getCategoryById.ExecuteAsync(id);

        if (category is null)
            return NotFound();

        var viewModel = _mapper.Map<EditCategoryViewModel>(category);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(EditCategoryViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var dto = _mapper.Map<UpdateCategoryDto>(viewModel);

        var updated = await _updateCategory.ExecuteAsync(dto);

        if (!updated)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _getCategoryById.ExecuteAsync(id);

        if (category is null)
            return NotFound();

        var viewModel = _mapper.Map<CategoryDetailsViewModel>(category);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _deleteCategory.ExecuteAsync(id);

        if (!deleted)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
