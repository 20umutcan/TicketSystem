using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;
using TicketAPI.Repositories;

namespace TicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyPartsController : ControllerBase
    {
        private readonly IRepository<WarrantyPart> _repository;
        private readonly TicketDbContext _context;

        public WarrantyPartsController(IRepository<WarrantyPart> repository, TicketDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parts = await _context.WarrantyParts
                .Include(w => w.Product)
                .ToListAsync();
            return Ok(parts);
        }

        [HttpGet("byproduct/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var parts = await _context.WarrantyParts
                .Where(w => w.ProductId == productId)
                .ToListAsync();
            return Ok(parts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var part = await _repository.GetByIdAsync(id);
            if (part == null)
                return NotFound();
            return Ok(part);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WarrantyPart part)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(part);
            await _repository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = part.Id }, part);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WarrantyPart part)
        {
            if (id != part.Id)
                return BadRequest();

            await _repository.UpdateAsync(part);
            await _repository.SaveChangesAsync();
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
