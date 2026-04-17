# Teknik Destek (Ticket) Sistemi

---

## Proje Mimarisi

```
TicketSystem/
├── TicketAPI/          → ASP.NET Core Web API (Entity Framework Core + SQLite)
│   ├── Controllers/    → CategoriesController, CustomersController, ProductsController, TicketsController
│   ├── Models/         → Category, Customer, Product, Ticket, TicketDbContext
│   ├── Repositories/   → IRepository<T>, Repository<T>  (Generic Repository Pattern)
│   └── Migrations/     → EF Core Code-First migration
│
└── TicketMVC/          → ASP.NET Core MVC (HttpClient Service Layer)
    ├── Controllers/    → HomeController, CategoriesController, CustomersController, ProductsController, TicketsController
    ├── Models/         → Aynı entity modeller (API ile senkronize)
    ├── Services/       → ITicketService, TicketService (HttpClient ile API tüketimi)
    └── Views/          → Bootstrap 5.3 + DataTables.net (Modal CRUD, Sidebar, Dashboard)
```

---

## Teknoloji Altyapısı

| Katman | Teknoloji |
|--------|-----------|
| ORM | Entity Framework Core 8.0 |
| Veritabanı | SQLite (Code-First, Migration) |
| Web API | ASP.NET Core 8.0 Web API |
| MVC İstemci | ASP.NET Core 8.0 MVC |
| HTTP İletişim | `HttpClient` + `System.Net.Http.Json` |
| UI Framework | Bootstrap 5.3 |
| Tablo Bileşeni | DataTables.net 1.13.4 |
| Desen | Generic Repository Pattern |

---

## Veritabanı İlişkileri

```
Category (1) ──────── (N) Product (1) ──────── (N) Ticket
Customer (1) ──────────────────────────────── (N) Ticket
```

- Bir **Kategori** altında birçok **Ürün** tanımlanabilir.
- Bir **Ürün** birçok **Arıza Kaydı (Ticket)** ile ilişkilendirilebilir.
- Bir **Müşteri** birçok **Arıza Kaydı** açabilir.

---

## API Endpoint'leri

Swagger UI: `http://localhost:5062/swagger`

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/customers` | Tüm müşterileri listeler |
| POST | `/api/customers` | Yeni müşteri ekler |
| PUT | `/api/customers/{id}` | Müşteriyi günceller |
| DELETE | `/api/customers/{id}` | Müşteriyi siler |
| GET | `/api/categories` | Tüm kategorileri listeler |
| POST | `/api/categories` | Yeni kategori ekler |
| PUT | `/api/categories/{id}` | Kategoriyi günceller |
| DELETE | `/api/categories/{id}` | Kategoriyi siler |
| GET | `/api/products` | Tüm ürünleri listeler (Category dahil) |
| POST | `/api/products` | Yeni ürün ekler |
| PUT | `/api/products/{id}` | Ürünü günceller |
| DELETE | `/api/products/{id}` | Ürünü siler |
| GET | `/api/tickets` | Tüm arıza kayıtlarını listeler |
| POST | `/api/tickets` | Yeni arıza kaydı ekler |
| PUT | `/api/tickets/{id}` | Arıza kaydını günceller |
| DELETE | `/api/tickets/{id}` | Arıza kaydını siler |

---

## Çalıştırma

İki terminalde sırasıyla çalıştırın:

```powershell
# Terminal 1 — API
cd TicketAPI
dotnet run

# Terminal 2 — MVC
cd TicketMVC
dotnet run
```

- API: http://localhost:5062
- MVC: http://localhost:5106

---

## Clean Code Prensipleri

- **Repository Pattern**: `IRepository<T>` ve `Repository<T>` ile EF Core erişimi soyutlanmıştır.
- **Service Layer**: `ITicketService` ve `TicketService` ile API haberleşmesi MVC controller'lardan ayrılmıştır.
- **DI (Dependency Injection)**: Tüm bağımlılıklar constructor injection ile çözülmektedir.
- **Separation of Concerns**: API, veri katmanını; MVC, sunum katmanını yönetir.
- **JSON Cycle Koruması**: `ReferenceHandler.IgnoreCycles` ile navigation property döngüleri önlenmiştir.

---

## Yapay Zeka Araçları Kullanımı

Bu projede **GitHub Copilot (Claude Sonnet)** kullanılmıştır.  
Kullanılan teknikler:
- **Bütüncül Proje Tasarımı**: Tüm mimari gereksinimler tek bir prompt zinciriyle tanımlandı.
- **Direktif Tabanlı İstek**: Her bileşenin ne yapması gerektiği net direktiflerle belirtildi (Clean Code, Repository Pattern, WinForms UI vb.).
- **Adımlı Doğrulama**: Her build adımında hata kontrolü yapıldı ve gerekli düzeltmeler uygulandı.
