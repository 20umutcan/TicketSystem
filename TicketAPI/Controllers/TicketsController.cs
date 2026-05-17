using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;
using TicketAPI.Repositories;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IRepository<Ticket> _repository;
        private readonly TicketDbContext _context;

        public TicketsController(IRepository<Ticket> repository, TicketDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Product)
                    .ThenInclude(p => p.Category)
                .OrderByDescending(t => t.Id)
                .ToListAsync();
            return Ok(tickets);
        }

        [HttpGet("archived")]
        public async Task<IActionResult> GetArchived()
        {
            var tickets = await _context.Tickets
                .Where(t => t.IsArchived)
                .Include(t => t.Product)
                .OrderByDescending(t => t.Id)
                .ToListAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Product)
                    .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ticket.CreatedDate = DateTime.UtcNow;
            await _repository.AddAsync(ticket);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mevcut kaydı veritabanından bul (navigation propertysiz)
            var existingTicket = await _context.Tickets.FindAsync(id);
            if (existingTicket == null)
                return NotFound();

            // Sadece scalar alanları güncelle (Product, Category gibi navigation property'leri yok say)
            existingTicket.Title = ticket.Title;
            existingTicket.Description = ticket.Description;
            existingTicket.CustomerName = ticket.CustomerName;
            existingTicket.ContactPhone = ticket.ContactPhone;
            existingTicket.Status = ticket.Status;
            existingTicket.Priority = ticket.Priority;
            existingTicket.IsArchived = ticket.IsArchived;
            existingTicket.TechnicianName = ticket.TechnicianName;
            existingTicket.ProductId = ticket.ProductId;
            // CreatedDate olduğu gibi kalsın

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        
    }
}