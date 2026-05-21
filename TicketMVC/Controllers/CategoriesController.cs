using Microsoft.AspNetCore.Mvc;
using TicketMVC.Models;
using TicketMVC.Services;

namespace TicketMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ITicketService _service;

        public CategoriesController(ITicketService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Kategoriler";
            var categories = await _service.GetCategoriesAsync();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteCategoryAsync(id);
                TempData["SuccessMessage"] = "Kategori başarıyla silindi.";
            }
            catch (HttpRequestException)
            {
                TempData["ErrorMessage"] = "Bu kategoriye bağlı ürünler bulunduğu için silme işlemi gerçekleştirilemiyor. Lütfen önce ilişkili ürünleri silin.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Silme işlemi sırasında beklenmeyen bir hata oluştu.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
