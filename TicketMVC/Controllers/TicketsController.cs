using Microsoft.AspNetCore.Mvc;
using TicketMVC.Models;
using TicketMVC.Services;

namespace TicketMVC.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Arıza Kayıtları";
            var tickets = (await _service.GetTicketsAsync()).Where(t => !t.IsArchived);
            ViewBag.Products = await _service.GetProductsAsync();
            ViewBag.Categories = await _service.GetCategoriesAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Archive()
        {
            ViewData["Title"] = "Arşiv";
            var tickets = await _service.GetArchivedTicketsAsync();
            return View(tickets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Ticket ticket, string? NewCategoryName, string? NewProductName)
        {
            // Kategori ve ürün çözümleme
            var success = await ResolveCategoryAndProduct(ticket, NewCategoryName, NewProductName);
            if (!success)
            {
                return RedirectToAction(nameof(Index));
            }

            // ModelState'ten ilgili alanları kaldır (null veya eksik olabilir)
            ModelState.Remove("ProductId");
            ModelState.Remove("Product");
            ModelState.Remove("NewCategoryName");
            ModelState.Remove("NewProductName");

            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                TempData["ErrorMessage"] = $"Form geçersiz: {errors}";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _service.CreateTicketAsync(ticket);
                TempData["SuccessMessage"] = "Kayıt başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kayıt oluşturulurken hata: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Ticket ticket, string? NewCategoryName, string? NewProductName)
        {
            var success = await ResolveCategoryAndProduct(ticket, NewCategoryName, NewProductName);
            if (!success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.Remove("ProductId");
            ModelState.Remove("Product");
            ModelState.Remove("NewCategoryName");
            ModelState.Remove("NewProductName");

            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                TempData["ErrorMessage"] = $"Form geçersiz: {errors}";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _service.UpdateTicketAsync(ticket);
                TempData["SuccessMessage"] = "Kayıt başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Güncelleme hatası: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleArchive(int id, bool isArchived, bool currentlyArchived)
        {
            var ticket = await _service.GetTicketAsync(id);
            if (ticket != null)
            {
                ticket.IsArchived = !currentlyArchived;
                await _service.UpdateTicketAsync(ticket);
            }
            return currentlyArchived
                ? RedirectToAction(nameof(Archive))
                : RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ResolveCategoryAndProduct(Ticket ticket, string? categoryName, string? productName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(productName))
                {
                    TempData["ErrorMessage"] = "Kategori ve ürün adı boş olamaz.";
                    return false;
                }

                var categories = await _service.GetCategoriesAsync();
                var cat = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                if (cat == null)
                {
                    cat = new Category { Name = categoryName };
                    await _service.CreateCategoryAsync(cat);
                    categories = await _service.GetCategoriesAsync();
                    cat = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                    if (cat == null)
                    {
                        TempData["ErrorMessage"] = $"Kategori oluşturulamadı: {categoryName}";
                        return false;
                    }
                }

                var products = await _service.GetProductsAsync();
                var prod = products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase) && p.CategoryId == cat.Id);
                if (prod == null)
                {
                    prod = new Product { Name = productName, CategoryId = cat.Id };
                    await _service.CreateProductAsync(prod);
                    products = await _service.GetProductsAsync();
                    prod = products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase) && p.CategoryId == cat.Id);
                    if (prod == null)
                    {
                        TempData["ErrorMessage"] = $"Ürün oluşturulamadı: {productName}";
                        return false;
                    }
                }

                ticket.ProductId = prod.Id;
                return true;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Kategori/Ürün işlenirken hata: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"HATA: {ex}");
                return false;
            }
        }
    }
}