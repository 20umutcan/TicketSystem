using System.ComponentModel.DataAnnotations;

namespace TicketMVC.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [StringLength(30)]
        public string? ContactPhone { get; set; }

        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public TicketPriority Priority { get; set; } = TicketPriority.Normal;

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int ProductId { get; set; }

        // Navigation property
        public Product? Product { get; set; }
    }
}