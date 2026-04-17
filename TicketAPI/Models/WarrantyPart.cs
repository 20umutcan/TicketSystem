using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
    /// <summary>
    /// Bir ürüne ait garanti kapsamındaki parça ve garanti süresi bilgisi.
    /// Her ürün kendi parça listesine sahiptir (veritabanı tabanlı).
    /// </summary>
    public class WarrantyPart
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string PartName { get; set; } = string.Empty;

        /// <summary>Garanti süresi (ay cinsinden)</summary>
        public int WarrantyMonths { get; set; }

        // Navigation property
        public Product? Product { get; set; }
    }
}
