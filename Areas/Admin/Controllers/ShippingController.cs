using Microsoft.AspNetCore.Mvc;
using Pronia.Contexts;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShippingController(AppDbContext _context) : Controller
	{

		
		public IActionResult Index()
		{
			var shippings = _context.Shippings.ToList();

			return View(shippings);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateShipping(Shipping shipping) {
			if (ModelState.IsValid == false)
			{
				return View();
			}
			_context.Shippings.Add(shipping);	
			_context.SaveChanges();
			return RedirectToAction("Index");

		}
		public IActionResult Delete(int id)
		{
			var shipping = _context.Shippings.Find(id);
			if (shipping == null)
			{
				return NotFound();
			}
			_context.Shippings.Remove(shipping);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			if (!ModelState.IsValid)
				return View();
			var shipping = _context.Shippings.Find(id);

			if (shipping is null)
				return NotFound();
			return View(shipping);
		}

		[HttpPost]

		public IActionResult Update(Shipping shipping)
		{
			var existShipping = _context.Sliders.Find(shipping.Id);

			if (existShipping is null)
				return NotFound();

			existShipping.Title =  shipping.Title;
			existShipping.Description = shipping.Description;
			existShipping.ImageUrl = shipping.ImageUrl;

			_context.Sliders.Update(existShipping);
			_context.SaveChanges();
		

			return RedirectToAction(nameof(Index));
		}

	}
}
