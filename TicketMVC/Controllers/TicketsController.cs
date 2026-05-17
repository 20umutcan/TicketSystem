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
        public async Task<IActionResult> Create([FromForm] Ticket ticket, string NewCategoryName, string NewProductName)
        {
            await ResolveCategoryAndProduct(ticket, NewCategoryName, NewProductName);
            ModelState.Remove("ProductId");
            ModelState.ClearValidationState("ProductId");
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = "Formda hatalar var: " + string.Join(" ", errors);
                return RedirectToAction(nameof(Index));
            }

            await _service.CreateTicketAsync(ticket);
            TempData["SuccessMessage"] = "Kayıt başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Ticket ticket, string NewCategoryName, string NewProductName)
        {
            await ResolveCategoryAndProduct(ticket, NewCategoryName, NewProductName);
            ModelState.Remove("ProductId");
            ModelState.ClearValidationState("ProductId");
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = "Formda hatalar var: " + string.Join(" ", errors);
                return RedirectToAction(nameof(Index));
            }

            await _service.UpdateTicketAsync(ticket);
            TempData["SuccessMessage"] = "Kayıt başarıyla güncellendi.";
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
        private async Task ResolveCategoryAndProduct(Ticket ticket, string categoryName, string productName)
        {
            if (!string.IsNullOrWhiteSpace(categoryName) && !string.IsNullOrWhiteSpace(productName))
            {
                var categories = await _service.GetCategoriesAsync();
                var cat = Enumerable.FirstOrDefault(categories, c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                if (cat == null)
                {
                    cat = new Category { Name = categoryName };
                    await _service.CreateCategoryAsync(cat);
                    cat = Enumerable.FirstOrDefault(await _service.GetCategoriesAsync(), c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                }

                if (cat != null)
                {
                    var products = await _service.GetProductsAsync();
                    var prod = Enumerable.FirstOrDefault(products, p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase) && p.CategoryId == cat.Id);
                    if (prod == null)
                    {
                        prod = new Product { Name = productName, CategoryId = cat.Id };
                        await _service.CreateProductAsync(prod);
                        prod = Enumerable.FirstOrDefault(await _service.GetProductsAsync(), p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase) && p.CategoryId == cat.Id);
                    }
                    if (prod != null)
                    {
                        ticket.ProductId = prod.Id;
                    }
                }
            }
        }
    }
}


