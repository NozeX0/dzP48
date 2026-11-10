using Microsoft.AspNetCore.Mvc;
using dz_3.Models;

namespace dz_3.Controllers;

public class CarsController : Controller
{
    public IActionResult Index()
    {
        var cars = new List<Car>
        {
            new Car { Brand = "Toyota", Model = "Camry", Price = 25000 },
            new Car { Brand = "BMW", Model = "X5", Price = 60000 },
            new Car { Brand = "Tesla", Model = "Model 3", Price = 45000 },
            new Car { Brand = "Ford", Model = "Mustang", Price = 55000 }
        };
        return View(cars);
    }
}