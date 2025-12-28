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


    [HttpGet]
    public IActionResult Create()
    {
        var categories = _context.Categories.ToList();
        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        var categories = _context.Categories.ToList();
        ViewBag.Categories = categories;
        if (!ModelState.IsValid)
            return View(product);

        var isExistCategory = _context.Products.Any(x => x.Id == product.CategoryId);

        if(isExistCategory)
        {
            ModelState.AddModelError("", "This category is unavaible");
            return View(product);
        }

        _context.Products.Add(product);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

}

