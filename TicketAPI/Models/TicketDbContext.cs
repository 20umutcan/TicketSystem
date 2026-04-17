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

            // ─── SEED DATA ───────────────────────────────────────────────

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Dizüstü Bilgisayar", Description = "Taşınabilir notebook ve laptop modelleri" },
                new Category { Id = 2, Name = "Masaüstü Bilgisayar", Description = "Kasa ve all-in-one masaüstü sistemler" },
                new Category { Id = 3, Name = "Yazıcı & Tarayıcı", Description = "Lazer, inkjet yazıcılar ve tarayıcı cihazlar" },
                new Category { Id = 4, Name = "Monitör", Description = "LCD, LED ve OLED ekranlar" },
                new Category { Id = 5, Name = "Ağ Cihazları", Description = "Router, switch, access point ve modem" }
            );

            // Products
            modelBuilder.Entity<Product>().HasData(
                // Notebooks
                new Product { Id = 1, CategoryId = 1, Name = "Asus VivoBook 15 X1502", Description = "Intel Core i5-1235U, 8GB DDR4, 512GB SSD, 15.6\" FHD IPS, Windows 11" },
                new Product { Id = 2, CategoryId = 1, Name = "Lenovo IdeaPad 3 15ITL6", Description = "Intel Core i7-1165G7, 16GB DDR4, 512GB SSD, 15.6\" FHD, Windows 11 Home" },
                new Product { Id = 3, CategoryId = 1, Name = "HP Pavilion 15-eg2049nt", Description = "Intel Core i5-1235U, 8GB DDR4, 256GB SSD+1TB HDD, 15.6\" FHD, Windows 11" },
                new Product { Id = 4, CategoryId = 1, Name = "Acer Aspire 5 A515-57G", Description = "Intel Core i5-1235U, 8GB DDR4, 512GB SSD, NVIDIA MX550, 15.6\" FHD" },
                // Desktops
                new Product { Id = 5, CategoryId = 2, Name = "Dell OptiPlex 3000 MT", Description = "Intel Core i5-12500, 8GB DDR4, 256GB SSD, Windows 11 Pro, kurumsal kullanım" },
                new Product { Id = 6, CategoryId = 2, Name = "HP EliteDesk 800 G9 SFF", Description = "Intel Core i7-12700, 16GB DDR5, 512GB SSD NVMe, Windows 11 Pro" },
                new Product { Id = 7, CategoryId = 2, Name = "Lenovo ThinkCentre M70s", Description = "Intel Core i5-10400, 8GB DDR4, 256GB SSD, DVD-RW, Windows 10 Pro" },
                // Printers
                new Product { Id = 8, CategoryId = 3, Name = "Brother DCP-L2540DW", Description = "Çok Fonksiyonlu Lazer Yazıcı, Dubleks, Wi-Fi, Tarayıcı" },
                new Product { Id = 9, CategoryId = 3, Name = "HP LaserJet Pro MFP M428fdw", Description = "Lazer, Faks, Tarayıcı, Fotokopi, Çift Taraflı Baskı, Ethernet/Wi-Fi" },
                new Product { Id = 10, CategoryId = 3, Name = "Epson EcoTank ET-2800", Description = "Renkli mürekkep tanklı yazıcı, Wi-Fi, tarayıcı, düşük maliyet" },
                // Monitors
                new Product { Id = 11, CategoryId = 4, Name = "Samsung 27\" Odyssey G3 S27AG320", Description = "27\" VA, 1080p, 165Hz, 1ms, FreeSync Premium, oyun monitörü" },
                new Product { Id = 12, CategoryId = 4, Name = "LG 24MK430H-B", Description = "24\" IPS, 1080p, 75Hz, AMD FreeSync, ince çerçeve, ofis kullanımı" },
                // Network
                new Product { Id = 13, CategoryId = 5, Name = "TP-Link Archer AX55", Description = "Wi-Fi 6, AX3000, 4 anten, Gigabit, beamforming, EasyMesh destekli" },
                new Product { Id = 14, CategoryId = 5, Name = "Cisco SG300-10PP 10-Port", Description = "10-Port Yönetilebilir PoE Gigabit Switch, Layer 3, VLAN, QoS" }
            );

            // WarrantyParts — her ürüne özel garanti parça listesi
            modelBuilder.Entity<WarrantyPart>().HasData(
                // Asus VivoBook 15
                new WarrantyPart { Id = 1, ProductId = 1, PartName = "Anakart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 2, ProductId = 1, PartName = "RAM", WarrantyMonths = 24 },
                new WarrantyPart { Id = 3, ProductId = 1, PartName = "SSD", WarrantyMonths = 24 },
                new WarrantyPart { Id = 4, ProductId = 1, PartName = "Ekran Paneli", WarrantyMonths = 12 },
                new WarrantyPart { Id = 5, ProductId = 1, PartName = "Batarya", WarrantyMonths = 12 },
                new WarrantyPart { Id = 6, ProductId = 1, PartName = "Klavye", WarrantyMonths = 12 },
                // Lenovo IdeaPad 3
                new WarrantyPart { Id = 7, ProductId = 2, PartName = "Anakart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 8, ProductId = 2, PartName = "RAM", WarrantyMonths = 24 },
                new WarrantyPart { Id = 9, ProductId = 2, PartName = "SSD", WarrantyMonths = 24 },
                new WarrantyPart { Id = 10, ProductId = 2, PartName = "Ekran Paneli", WarrantyMonths = 12 },
                new WarrantyPart { Id = 11, ProductId = 2, PartName = "Batarya", WarrantyMonths = 6 },
                new WarrantyPart { Id = 12, ProductId = 2, PartName = "Şarj Adaptörü", WarrantyMonths = 6 },
                // HP Pavilion 15
                new WarrantyPart { Id = 13, ProductId = 3, PartName = "Anakart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 14, ProductId = 3, PartName = "RAM", WarrantyMonths = 24 },
                new WarrantyPart { Id = 15, ProductId = 3, PartName = "SSD", WarrantyMonths = 24 },
                new WarrantyPart { Id = 16, ProductId = 3, PartName = "HDD", WarrantyMonths = 12 },
                new WarrantyPart { Id = 17, ProductId = 3, PartName = "Batarya", WarrantyMonths = 12 },
                new WarrantyPart { Id = 18, ProductId = 3, PartName = "Klavye", WarrantyMonths = 12 },
                new WarrantyPart { Id = 19, ProductId = 3, PartName = "Ekran Menteşe", WarrantyMonths = 24 },
                // Acer Aspire 5 (ekran kartı var - MX550)
                new WarrantyPart { Id = 20, ProductId = 4, PartName = "Anakart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 21, ProductId = 4, PartName = "Ekran Kartı (NVIDIA MX550)", WarrantyMonths = 24 },
                new WarrantyPart { Id = 22, ProductId = 4, PartName = "RAM", WarrantyMonths = 24 },
                new WarrantyPart { Id = 23, ProductId = 4, PartName = "SSD", WarrantyMonths = 24 },
                new WarrantyPart { Id = 24, ProductId = 4, PartName = "Batarya", WarrantyMonths = 12 },
                new WarrantyPart { Id = 25, ProductId = 4, PartName = "Fan / Soğutma", WarrantyMonths = 12 },
                // Dell OptiPlex 3000
                new WarrantyPart { Id = 26, ProductId = 5, PartName = "Anakart", WarrantyMonths = 36 },
                new WarrantyPart { Id = 27, ProductId = 5, PartName = "İşlemci", WarrantyMonths = 36 },
                new WarrantyPart { Id = 28, ProductId = 5, PartName = "RAM", WarrantyMonths = 36 },
                new WarrantyPart { Id = 29, ProductId = 5, PartName = "SSD", WarrantyMonths = 36 },
                new WarrantyPart { Id = 30, ProductId = 5, PartName = "Güç Kaynağı", WarrantyMonths = 12 },
                // HP EliteDesk 800 G9
                new WarrantyPart { Id = 31, ProductId = 6, PartName = "Anakart", WarrantyMonths = 36 },
                new WarrantyPart { Id = 32, ProductId = 6, PartName = "İşlemci", WarrantyMonths = 36 },
                new WarrantyPart { Id = 33, ProductId = 6, PartName = "RAM DDR5", WarrantyMonths = 36 },
                new WarrantyPart { Id = 34, ProductId = 6, PartName = "SSD NVMe", WarrantyMonths = 36 },
                new WarrantyPart { Id = 35, ProductId = 6, PartName = "Güç Kaynağı", WarrantyMonths = 12 },
                // Lenovo ThinkCentre M70s
                new WarrantyPart { Id = 36, ProductId = 7, PartName = "Anakart", WarrantyMonths = 36 },
                new WarrantyPart { Id = 37, ProductId = 7, PartName = "İşlemci", WarrantyMonths = 36 },
                new WarrantyPart { Id = 38, ProductId = 7, PartName = "RAM", WarrantyMonths = 24 },
                new WarrantyPart { Id = 39, ProductId = 7, PartName = "SSD", WarrantyMonths = 24 },
                new WarrantyPart { Id = 40, ProductId = 7, PartName = "DVD-RW Sürücü", WarrantyMonths = 12 },
                // Brother DCP-L2540DW
                new WarrantyPart { Id = 41, ProductId = 8, PartName = "Ana Kart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 42, ProductId = 8, PartName = "Lazer Drum Ünitesi", WarrantyMonths = 12 },
                new WarrantyPart { Id = 43, ProductId = 8, PartName = "Fuser (Isıtma) Ünitesi", WarrantyMonths = 12 },
                new WarrantyPart { Id = 44, ProductId = 8, PartName = "Kağıt Besleme Mekanizması", WarrantyMonths = 12 },
                // HP LaserJet Pro MFP M428fdw
                new WarrantyPart { Id = 45, ProductId = 9, PartName = "Ana Kart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 46, ProductId = 9, PartName = "Fuser Ünitesi", WarrantyMonths = 12 },
                new WarrantyPart { Id = 47, ProductId = 9, PartName = "Drum Ünitesi", WarrantyMonths = 12 },
                new WarrantyPart { Id = 48, ProductId = 9, PartName = "ADF (Otomatik Belge Besleyici)", WarrantyMonths = 12 },
                new WarrantyPart { Id = 49, ProductId = 9, PartName = "Faks Modülü", WarrantyMonths = 24 },
                // Epson EcoTank ET-2800
                new WarrantyPart { Id = 50, ProductId = 10, PartName = "Ana Kart", WarrantyMonths = 24 },
                new WarrantyPart { Id = 51, ProductId = 10, PartName = "Mürekkep Pompa Sistemi", WarrantyMonths = 12 },
                new WarrantyPart { Id = 52, ProductId = 10, PartName = "Baskı Kafası", WarrantyMonths = 12 },
                // Samsung Odyssey G3
                new WarrantyPart { Id = 53, ProductId = 11, PartName = "VA Panel", WarrantyMonths = 36 },
                new WarrantyPart { Id = 54, ProductId = 11, PartName = "Güç Kaynağı Kartı", WarrantyMonths = 24 },
                new WarrantyPart { Id = 55, ProductId = 11, PartName = "Ana Kart", WarrantyMonths = 24 },
                // LG 24MK430H
                new WarrantyPart { Id = 56, ProductId = 12, PartName = "IPS Panel", WarrantyMonths = 36 },
                new WarrantyPart { Id = 57, ProductId = 12, PartName = "Güç Kaynağı", WarrantyMonths = 24 },
                new WarrantyPart { Id = 58, ProductId = 12, PartName = "Ana Kart", WarrantyMonths = 24 },
                // TP-Link Archer AX55
                new WarrantyPart { Id = 59, ProductId = 13, PartName = "Ana Kart / CPU", WarrantyMonths = 24 },
                new WarrantyPart { Id = 60, ProductId = 13, PartName = "Wi-Fi 6 Modülü", WarrantyMonths = 24 },
                new WarrantyPart { Id = 61, ProductId = 13, PartName = "Güç Adaptörü", WarrantyMonths = 12 },
                // Cisco SG300-10PP
                new WarrantyPart { Id = 62, ProductId = 14, PartName = "Switching Fabric", WarrantyMonths = 36 },
                new WarrantyPart { Id = 63, ProductId = 14, PartName = "PoE Güç Modülü", WarrantyMonths = 24 },
                new WarrantyPart { Id = 64, ProductId = 14, PartName = "Fan", WarrantyMonths = 12 }
            );
        }
    }
}
