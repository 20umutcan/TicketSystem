using Microsoft.AspNetCore.Mvc;

namespace TicketMVC.Controllers
{
    // Müşteri yönetimi kaldırıldı.
    // Müşteri adı artık arıza kaydında serbest metin olarak girilmektedir.
    public class CustomersController : Controller
    {
        public IActionResult Index() => RedirectToAction("Index", "Tickets");
    }
}

