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
        public async Task<IActionResult> Create([FromForm] Ticket ticket)
        {
            await _service.CreateTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Ticket ticket)
        {
            await _service.UpdateTicketAsync(ticket);
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
    }
}

