using Microsoft.AspNetCore.Mvc;
using TicketMVC.Models;
using TicketMVC.Services;

namespace TicketMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ITicketService _service;

        public ProductsController(ITicketService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Ürünler";
            var products = await _service.GetProductsAsync();
            ViewBag.Categories = await _service.GetCategoriesAsync();
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            await _service.CreateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
            }
            catch (HttpRequestException)
            {
                TempData["ErrorMessage"] = "Bu ürüne bağlı arıza kayıtları bulunduğu için silme işlemi gerçekleştirilemiyor. Lütfen önce ilişkili kayıtları silin.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Silme işlemi sırasında beklenmeyen bir hata oluştu.";
            }
            return RedirectToAction(nameof(Index));
        }

        
    }
}


