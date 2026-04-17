using System.ComponentModel.DataAnnotations;

namespace TicketMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        // Navigation property
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}