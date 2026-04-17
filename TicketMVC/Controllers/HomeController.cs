using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketMVC.Models;
using TicketMVC.Services;

namespace TicketMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITicketService _ticketService;

    public HomeController(ILogger<HomeController> logger, ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _ticketService.GetCategoriesAsync();
        var products = await _ticketService.GetProductsAsync();
        var tickets = (await _ticketService.GetTicketsAsync()).ToList();

        ViewBag.CategoriesCount = categories.Count();
        ViewBag.ProductsCount = products.Count();
        ViewBag.TicketsCount = tickets.Count;
        ViewBag.OpenTicketsCount = tickets.Count(t => t.Status == TicketStatus.Open && !t.IsArchived);
        ViewBag.InProgressCount = tickets.Count(t => t.Status == TicketStatus.InProgress && !t.IsArchived);
        ViewBag.CriticalCount = tickets.Count(t => t.Priority == TicketPriority.Critical && !t.IsArchived);
        ViewBag.RecentTickets = tickets.Take(5).ToList();

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
