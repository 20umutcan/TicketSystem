using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketAPI.Migrations
{
    /// <inheritdoc />
    public partial class V2_RedesignWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ContactPhone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    WarrantyMonths = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantyParts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Taşınabilir notebook ve laptop modelleri", "Dizüstü Bilgisayar" },
                    { 2, "Kasa ve all-in-one masaüstü sistemler", "Masaüstü Bilgisayar" },
                    { 3, "Lazer, inkjet yazıcılar ve tarayıcı cihazlar", "Yazıcı & Tarayıcı" },
                    { 4, "LCD, LED ve OLED ekranlar", "Monitör" },
                    { 5, "Router, switch, access point ve modem", "Ağ Cihazları" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Intel Core i5-1235U, 8GB DDR4, 512GB SSD, 15.6\" FHD IPS, Windows 11", "Asus VivoBook 15 X1502" },
                    { 2, 1, "Intel Core i7-1165G7, 16GB DDR4, 512GB SSD, 15.6\" FHD, Windows 11 Home", "Lenovo IdeaPad 3 15ITL6" },
                    { 3, 1, "Intel Core i5-1235U, 8GB DDR4, 256GB SSD+1TB HDD, 15.6\" FHD, Windows 11", "HP Pavilion 15-eg2049nt" },
                    { 4, 1, "Intel Core i5-1235U, 8GB DDR4, 512GB SSD, NVIDIA MX550, 15.6\" FHD", "Acer Aspire 5 A515-57G" },
                    { 5, 2, "Intel Core i5-12500, 8GB DDR4, 256GB SSD, Windows 11 Pro, kurumsal kullanım", "Dell OptiPlex 3000 MT" },
                    { 6, 2, "Intel Core i7-12700, 16GB DDR5, 512GB SSD NVMe, Windows 11 Pro", "HP EliteDesk 800 G9 SFF" },
                    { 7, 2, "Intel Core i5-10400, 8GB DDR4, 256GB SSD, DVD-RW, Windows 10 Pro", "Lenovo ThinkCentre M70s" },
                    { 8, 3, "Çok Fonksiyonlu Lazer Yazıcı, Dubleks, Wi-Fi, Tarayıcı", "Brother DCP-L2540DW" },
                    { 9, 3, "Lazer, Faks, Tarayıcı, Fotokopi, Çift Taraflı Baskı, Ethernet/Wi-Fi", "HP LaserJet Pro MFP M428fdw" },
                    { 10, 3, "Renkli mürekkep tanklı yazıcı, Wi-Fi, tarayıcı, düşük maliyet", "Epson EcoTank ET-2800" },
                    { 11, 4, "27\" VA, 1080p, 165Hz, 1ms, FreeSync Premium, oyun monitörü", "Samsung 27\" Odyssey G3 S27AG320" },
                    { 12, 4, "24\" IPS, 1080p, 75Hz, AMD FreeSync, ince çerçeve, ofis kullanımı", "LG 24MK430H-B" },
                    { 13, 5, "Wi-Fi 6, AX3000, 4 anten, Gigabit, beamforming, EasyMesh destekli", "TP-Link Archer AX55" },
                    { 14, 5, "10-Port Yönetilebilir PoE Gigabit Switch, Layer 3, VLAN, QoS", "Cisco SG300-10PP 10-Port" }
                });

            migrationBuilder.InsertData(
                table: "WarrantyParts",
                columns: new[] { "Id", "PartName", "ProductId", "WarrantyMonths" },
                values: new object[,]
                {
                    { 1, "Anakart", 1, 24 },
                    { 2, "RAM", 1, 24 },
                    { 3, "SSD", 1, 24 },
                    { 4, "Ekran Paneli", 1, 12 },
                    { 5, "Batarya", 1, 12 },
                    { 6, "Klavye", 1, 12 },
                    { 7, "Anakart", 2, 24 },
                    { 8, "RAM", 2, 24 },
                    { 9, "SSD", 2, 24 },
                    { 10, "Ekran Paneli", 2, 12 },
                    { 11, "Batarya", 2, 6 },
                    { 12, "Şarj Adaptörü", 2, 6 },
                    { 13, "Anakart", 3, 24 },
                    { 14, "RAM", 3, 24 },
                    { 15, "SSD", 3, 24 },
                    { 16, "HDD", 3, 12 },
                    { 17, "Batarya", 3, 12 },
                    { 18, "Klavye", 3, 12 },
                    { 19, "Ekran Menteşe", 3, 24 },
                    { 20, "Anakart", 4, 24 },
                    { 21, "Ekran Kartı (NVIDIA MX550)", 4, 24 },
                    { 22, "RAM", 4, 24 },
                    { 23, "SSD", 4, 24 },
                    { 24, "Batarya", 4, 12 },
                    { 25, "Fan / Soğutma", 4, 12 },
                    { 26, "Anakart", 5, 36 },
                    { 27, "İşlemci", 5, 36 },
                    { 28, "RAM", 5, 36 },
                    { 29, "SSD", 5, 36 },
                    { 30, "Güç Kaynağı", 5, 12 },
                    { 31, "Anakart", 6, 36 },
                    { 32, "İşlemci", 6, 36 },
                    { 33, "RAM DDR5", 6, 36 },
                    { 34, "SSD NVMe", 6, 36 },
                    { 35, "Güç Kaynağı", 6, 12 },
                    { 36, "Anakart", 7, 36 },
                    { 37, "İşlemci", 7, 36 },
                    { 38, "RAM", 7, 24 },
                    { 39, "SSD", 7, 24 },
                    { 40, "DVD-RW Sürücü", 7, 12 },
                    { 41, "Ana Kart", 8, 24 },
                    { 42, "Lazer Drum Ünitesi", 8, 12 },
                    { 43, "Fuser (Isıtma) Ünitesi", 8, 12 },
                    { 44, "Kağıt Besleme Mekanizması", 8, 12 },
                    { 45, "Ana Kart", 9, 24 },
                    { 46, "Fuser Ünitesi", 9, 12 },
                    { 47, "Drum Ünitesi", 9, 12 },
                    { 48, "ADF (Otomatik Belge Besleyici)", 9, 12 },
                    { 49, "Faks Modülü", 9, 24 },
                    { 50, "Ana Kart", 10, 24 },
                    { 51, "Mürekkep Pompa Sistemi", 10, 12 },
                    { 52, "Baskı Kafası", 10, 12 },
                    { 53, "VA Panel", 11, 36 },
                    { 54, "Güç Kaynağı Kartı", 11, 24 },
                    { 55, "Ana Kart", 11, 24 },
                    { 56, "IPS Panel", 12, 36 },
                    { 57, "Güç Kaynağı", 12, 24 },
                    { 58, "Ana Kart", 12, 24 },
                    { 59, "Ana Kart / CPU", 13, 24 },
                    { 60, "Wi-Fi 6 Modülü", 13, 24 },
                    { 61, "Güç Adaptörü", 13, 12 },
                    { 62, "Switching Fabric", 14, 36 },
                    { 63, "PoE Güç Modülü", 14, 24 },
                    { 64, "Fan", 14, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProductId",
                table: "Tickets",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyParts_ProductId",
                table: "WarrantyParts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "WarrantyParts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
