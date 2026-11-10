using Microsoft.AspNetCore.Mvc;
using dz_4.Models;

namespace dz_4.Controllers;

public class CategoryMvcController : Controller
{
    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(CreateCategoryModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Title))
            ModelState.AddModelError("Title", "Title is required");

        if (!ModelState.IsValid) return View(model);

        return Content($"Category '{model.Title}' successfully created!");
    }
}