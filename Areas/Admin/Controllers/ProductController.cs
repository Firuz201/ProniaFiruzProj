using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;

namespace Pronia.Areas.Admin.Controllers;
[Area("Admin")]


public class ProductController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var products = _context.Products.Include(x=>x.Category).ToList ();

        return View(products);
    }
}
