# Teknik Destek (Ticket) Sistemi

Bu proje, küçük ve orta ölçekli iþletmelerin (bilgisayar teknik servisleri, tamirciler vb.) müþteri arýza kayýtlarýný yönetebilmesi için geliþtirilmiþ bir **CRM / Ticket** uygulamasýdýr. SOLID ve Clean Code prensiplerine uygun olarak, N-Tier (Çok Katmanlý) kod yapýsý hedeflenerek geliþtirilmiþtir.

---

## Proje Mimarisi

`
TicketSystem/
+¦¦ TicketAPI/          › ASP.NET Core Web API (Entity Framework Core + SQLite)
-   +¦¦ Controllers/    › CategoriesController, ProductsController, TicketsController
-   +¦¦ Models/         › Category, Product, Ticket, TicketDbContext
-   +¦¦ Repositories/   › IRepository<T>, Repository<T>  (Generic Repository Pattern)
-   L¦¦ Migrations/     › EF Core Code-First migration
-
L¦¦ TicketMVC/          › ASP.NET Core MVC (HttpClient Service Layer)
    +¦¦ Controllers/    › HomeController, CategoriesController, ProductsController, TicketsController
    +¦¦ Models/         › Ayný entity modeller (API ile senkronize)
    +¦¦ Services/       › ITicketService, TicketService (HttpClient ile API tüketimi)
    L¦¦ Views/          › Bootstrap 5.3 (Modal CRUD, Sidebar, Dashboard)
`

---

## Teknoloji Altyapýsý

| Katman | Teknoloji |
|--------|-----------|
| ORM | Entity Framework Core 8.0 |
| Veritabaný | SQLite (Code-First, Migration) |
| Web API | ASP.NET Core 8.0 Web API |
| MVC Ýstemci | ASP.NET Core 8.0 MVC |
| HTTP Ýletiþim | HttpClient + System.Net.Http.Json |
| UI Framework | Bootstrap 5.3 + Özel Validasyon Scriptleri (site.js) |
| Desenler | Generic Repository Pattern, Dependency Injection |

---

## Veritabaný Ýliþkileri (Entity Relations)

Sistemdeki tablolar ve iliþkiler (One-to-Many):

`
Category (1) ¦¦¦¦¦¦¦¦ (N) Product
Product  (1) ¦¦¦¦¦¦¦¦ (N) Ticket
`

- Bir **Kategori** altýnda birçok **Ürün** tanýmlanabilir.
- Bir **Ürün** için birçok **Arýza Kaydý (Ticket)** oluþturulabilir. Müþteri bilgileri direkt serbest metin olarak Ticket içerisine kaydedilir. (Küçük iþletmeler için hýzlý kayýt giriþini saðlamak adýna müþteri tablosu ayrýþtýrýlmamýþtýr, Ticket içerisinde barýndýrýlmaktadýr).

---

## API Endpoint'leri

Swagger UI: http://localhost:5062/swagger

| Method | Endpoint | Açýklama |
|--------|----------|----------|
| GET | /api/categories | Tüm kategorileri listeler |
| POST | /api/categories | Yeni kategori ekler |
| PUT | /api/categories/{id} | Kategoriyi günceller |
| DELETE | /api/categories/{id} | Kategoriyi siler |
| GET | /api/products | Tüm ürünleri listeler (Category dahil) |
| POST | /api/products | Yeni ürün ekler |
| PUT | /api/products/{id} | Ürünü günceller |
| DELETE | /api/products/{id} | Ürünü siler |
| GET | /api/tickets | Tüm arýza kayýtlarýný listeler |
| POST | /api/tickets | Yeni arýza kaydý ekler |
| PUT | /api/tickets/{id} | Arýza kaydýný günceller |
| DELETE | /api/tickets/{id} | Arýza kaydýný siler |

---

## Çýktý Kontrol Metodolojisi

1. **Birim (Katman) Testleri:** API tarafýndaki Endpoint'ler Swagger arayüzü üzerinden manuel olarak HTTP testlerine tabi tutulmuþ ve format doðruluðu test edilmiþtir.
2. **Kullanýcý Girdi Kontrolü (Client-Side & Server-Side Validation):** MVC View tarafýnda JS Regex maskeleri (telefon numarasýnýn özel formatý için) ve MVC ModelState kontrolleri ile geçersiz verilerin API'ye ulaþmadan reddedilmesi saðlandý.
3. **Database Bütünlüðü:** Code-First yaklaþýmý ile DB ayaða kalkarken \HasData\ metodu (Seed Data) kullanýlarak veritabaný boþken örnek verilerle doldurulmuþtur. Entity Framework iliþkilerindeki \Cascade\ ve \Restrict\ delete özellikleri doðrulandý.

---

## Kullaným Kýlavuzu

Uygulamayý çalýþtýrmak için iki konsol oturumu açýlmalýdýr.

\\\powershell
# Terminal 1 — API'yi Baþlatýn
cd TicketAPI
dotnet run
\\\
API baþladýktan sonra \http://localhost:5062/swagger\ üzerinden direkt test edilebilir.

\\\powershell
# Terminal 2 — MVC Arayüzünü Baþlatýn
cd TicketMVC
dotnet run
\\\
Arayüz baþladýktan sonra tarayýcý üzerinden \http://localhost:5106\ adresine giderek teknik servis arayüzünü kullanabilirsiniz.

Sol menüden:
- **Arýza Kayýtlarý:** Yeni müþteri kaydý açabilir, hatalý ürünlerin baþlýðýný ve telefon numarasýný iþleyebilirsiniz.
- **Ürünler & Kategoriler:** Tamirini yaptýðýnýz ürünlerin genel listesini düzenleyebilirsiniz.

---

## Kullanýlan Yapay Zeka Araçlarý

Bu projede **GitHub Copilot (Gemini 3.1 Pro (Preview))** kullanýlarak Refactoring, Code Clean-up ve Bug-Fixing gerçekleþtirilmiþtir.  
Kullanýlan teknikler:
- Proje içindeki gereksiz/ölü kodlarýn (müþteri tablosu gibi) silinip mimarinin 3 ana tablo üzerine optimize edilmesi ("Dead Code Elimination").
- UX iyileþtirmesi için JavaScript kullanýlarak anlýk "Input Mask" kodlanmasý (Prompt Engineering).
- SOLID ve Clean Architecture standartlarýna uygun biçimlendirme ve Türkçe hata mesajý özelleþtirmeleri yapýldý.

