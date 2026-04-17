using System.ComponentModel.DataAnnotations;

namespace TicketMVC.Models
{
    public class WarrantyPart
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string PartName { get; set; } = string.Empty;

        public int WarrantyMonths { get; set; }

        public Product? Product { get; set; }
    }
}
