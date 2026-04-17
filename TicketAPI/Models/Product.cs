using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<WarrantyPart> WarrantyParts { get; set; } = new List<WarrantyPart>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}