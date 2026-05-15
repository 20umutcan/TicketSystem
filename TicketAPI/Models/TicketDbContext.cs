using Microsoft.EntityFrameworkCore;

namespace TicketAPI.Models
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WarrantyPart> WarrantyParts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WarrantyPart>()
                .HasOne(w => w.Product)
                .WithMany(p => p.WarrantyParts)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                        modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Product)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data (Örnek Veriler)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Bilgisayar" },
                new Category { Id = 2, Name = "Akýllý Telefon" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 1, Name = "Dell XPS 15" },
                new Product { Id = 2, CategoryId = 2, Name = "iPhone 14 Pro" }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket 
                { 
                    Id = 1, 
                    Title = "Ekran Açýlmýyor", 
                    Description = "Cihazýn güç tuþuna basýldýðýnda tepki vermiyor.", 
                    CustomerName = "Ahmet Yýlmaz", 
                    ContactPhone = "0 555 123 4567", 
                    Status = TicketStatus.Open, 
                    Priority = TicketPriority.High, 
                    CreatedDate = new DateTime(2026, 5, 14, 10, 0, 0, DateTimeKind.Utc), 
                    ProductId = 1,
                    TechnicianName = "Umut Tekniker"
                },
                new Ticket 
                { 
                    Id = 2, 
                    Title = "Þarj Soketi Temassýzlýðý", 
                    Description = "Kabloyu takýnca bazen þarj alýyor bazen almýyor.", 
                    CustomerName = "Ayþe Kaya", 
                    ContactPhone = "0 532 987 6543", 
                    Status = TicketStatus.InProgress, 
                    Priority = TicketPriority.Normal, 
                    CreatedDate = new DateTime(2026, 5, 15, 9, 30, 0, DateTimeKind.Utc), 
                    ProductId = 2,
                    TechnicianName = "Ali Usta"
                }
            );
        }
    }
}

