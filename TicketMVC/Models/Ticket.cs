using System.ComponentModel.DataAnnotations;

namespace TicketMVC.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Müşteri adı zorunludur.")]
        [StringLength(150, ErrorMessage = "Müşteri adı en fazla 150 karakter olabilir.")]
        public string CustomerName { get; set; } = string.Empty;

        [RegularExpression(@"^(0[\s]?\(?([0-9]{3})\)?[\s-]?([0-9]{3})[\s-]?([0-9]{4}))?$", ErrorMessage = "Geçerli bir telefon numarası giriniz (Örn: 0 555 555 5555).")]
        [StringLength(30, ErrorMessage = "Telefon en fazla 30 karakter olabilir.")]
        public string? ContactPhone { get; set; }

        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public TicketPriority Priority { get; set; } = TicketPriority.Normal;

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        
        [StringLength(100, ErrorMessage = "Teknisyen ad� en fazla 100 karakter olabilir.")]
        public string? TechnicianName { get; set; }

        [Required]
        public int ProductId { get; set; }

        // Navigation property
        public Product? Product { get; set; }
    }
}
