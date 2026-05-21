# Teknik Destek (Ticket) Sistemi

Bu proje, küçük ve orta ölçekli işletmelerin (bilgisayar teknik servisleri, tamirciler, telefon tamircileri vb.) müşteri arıza kayıtlarını uçtan uca kolayca yönetebilmesi için geliştirilmiş bir **CRM / Ticket** uygulamasıdır. SOLID ve Clean Code prensiplerine uygun olarak, N-Tier (Çok Katmanlı) kod yapısı hedeflenerek geliştirilmiştir.

Proje sayesinde işletmeler, tamir ettikleri ürünleri ve kategorilerini sistemde tutabilir, yeni gelen arıza kayıtlarını öncelik ve durum bazlı (Açık, İşleniyor, Tamamlandı) olarak sınıflandırabilir ve onarımı biten cihazların kayıtlarını arşive kaldırarak temiz ve düzenli bir iş takip ekranına (Dashboard) sahip olabilirler.

---

## Proje Mimarisi

- 📁 **TicketSystem/**
  - 📁 **TicketAPI/** *(ASP.NET Core Web API / SQLite)*
    - 📂 \Controllers/\ - API Uç Noktaları (Categories, Products, Tickets)
    - 📂 \Models/\ - Veri Modelleri (Category, Product, Ticket vb.)
    - 📂 \Repositories/\ - Generic Repository Desen Yapısı
    - 📂 \Migrations/\ - Entity Framework Code-First Dosyaları
  - 📁 **TicketMVC/** *(ASP.NET Core MVC / İstemci Katmanı)*
    - 📂 \Controllers/\ - MVC Sayfa Yönlendirmeleri
    - 📂 \Models/\ - API ile Senkronize ViewModel'ler
    - 📂 \Services/\ - HttpClient Tabanlı API Tüketim Servisleri
    - 📂 \Views/\ - Bootstrap 5.3 Tema ve Kullanıcı Arayüzü

---

## Teknoloji Altyapısı

| Katman | Teknoloji |
|--------|-----------|
| ORM | Entity Framework Core 8.0 |
| Veritabanı | SQLite (Code-First, Migration) |
| Web API | ASP.NET Core 8.0 Web API |
| MVC İstemci | ASP.NET Core 8.0 MVC |
| HTTP İletişim | HttpClient + System.Net.Http.Json |
| UI Framework | Bootstrap 5.3 + Özel Validasyon Scriptleri (site.js) |
| Desenler | Generic Repository Pattern, Dependency Injection |

---

## Veritabanı İlişkileri (Entity Relations)

Sistemdeki tablolar ve ilişkiler (One-to-Many):

* \Category (1) ──────── (N) Product\
* \Product  (1) ──────── (N) Ticket\

- Bir **Kategori** altında birçok **Ürün** tanımlanabilir.
- Bir **Ürün** için birçok **Arıza Kaydı (Ticket)** oluşturulabilir. Küçük işletmeler için ayrı bir müşteri tablosu yükünden kaçınarak, müşteri bilgileri direkt serbest metin olarak Ticket içerisine kaydedilir.
- **Arıza Kayıtları (Ticket)** nesnesi aynı zamanda *Öncelik (Priority)*, *Durum (Status)*, *Arşiv (IsArchived)* gibi alanlara sahiptir.

---

## API Endpoint'leri

Swagger UI: http://localhost:5062/swagger

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | /api/categories | Tüm kategorileri listeler |
| POST | /api/categories | Yeni kategori ekler |
| PUT | /api/categories/{id} | Kategoriyi günceller |
| DELETE | /api/categories/{id} | Kategoriyi siler |
| GET | /api/products | Tüm ürünleri listeler (Category dahil) |
| POST | /api/products | Yeni ürün ekler |
| PUT | /api/products/{id} | Ürünü günceller |
| DELETE | /api/products/{id} | Ürünü siler |
| GET | /api/tickets | Tüm arıza kayıtlarını listeler |
| GET | /api/tickets/archived | Sadece arşivlenmiş kayıtları listeler |
| POST | /api/tickets | Yeni arıza kaydı ekler |
| PUT | /api/tickets/{id} | Arıza kaydını günceller |
| DELETE | /api/tickets/{id} | Arıza kaydını siler |

---

## Çıktı Kontrol Metodolojisi

1. **Birim (Katman) Testleri:** API tarafındaki Endpoint'ler Swagger arayüzü üzerinden manuel olarak HTTP testlerine tabi tutulmuş ve format doğruluğu test edilmiştir.
2. **Kullanıcı Girdi Kontrolü (Client-Side & Server-Side Validation):** MVC View tarafında JS Regex maskeleri ve MVC ModelState kontrolleri ile geçersiz verilerin API'ye ulaşmadan reddedilmesi sağlandı.
3. **Database Bütünlüğü:** Code-First yaklaşımı ile DB ayağa kalkarken \HasData\ metodu kullanılarak örnek verilerle doldurulmuştur. Entity Framework ilişkilerindeki \Cascade\ ve \Restrict\ delete özellikleri doğrulandı.

---

## Kullanım Kılavuzu

Uygulamayı çalıştırmak için iki konsol oturumu açılmalıdır.

\\\ash
# Terminal 1 - API'yi Başlatın
cd TicketAPI
dotnet run
\\\
API başladıktan sonra \http://localhost:5062/swagger\ üzerinden direkt test edilebilir.

\\\ash
# Terminal 2 - MVC Arayüzünü Başlatın
cd TicketMVC
dotnet run
\\\
Arayüz başladıktan sonra tarayıcı üzerinden \http://localhost:5106\ adresine giderek teknik servis arayüzünü kullanabilirsiniz.

Sol menüden:
- **Dashboard:** Açık, işlenen, kritik ve diğer istatistikleri genel bir ekranda görebilirsiniz.
- **Arıza Kayıtları:** Yeni müşteri kaydı açabilir, hatalı ürünlerin başlığını, telefon numarasını, teknisyen ve durum/öncelik atamasını yapabilirsiniz. Ayrıca tamamlanan işleri arşive kaldırabilirsiniz.
- **Arşiv:** Arşivlenmiş (gizlenen) arıza kayıtlarınıza erişebilirsiniz.
- **Ürünler & Kategoriler:** Tamirini yaptığınız ürünlerin genel listesini düzenleyebilirsiniz.

---

## Karşılaşılan Zorluklar ve Geliştirme Süreci (Zorlanılan Yerler)

Bu projeyi geliştirirken Yapay Zeka (GitHub Copilot / LLM) araçları yoğun olarak kullanılmıştır. Ancak süreç içerisinde karşılaşılan başlıca zorluklar şunlar olmuştur:

- **Yapay Zeka Müdahaleleri ve Yıkıcı (Breaking) Değişiklikler:** Yapay zekaya verilen prompt'lar sonucunda yeni özellikler eklenirken veya düzeltmeler yapılırken, o ana kadar problemsiz çalışan başka kısımların bozulduğu durumlar (regression) yaşandı. Çoğu zaman atılan yeni bir prompt'un daha önceden çalışan bir yeri bozması sebebiyle yapay zekanın sağladığı kodlara tamamen güvenilememiş ve bozulan kısımlara bazen elle müdahale etmek zorunda kalınmıştır.
- **Arayüz (UI) Tasarımını Oturtma Süreci:** Özellikle MVC tarafında Bootstrap 5.3 kullanılarak oluşturulan arayüz bileşenlerinin projeye tam anlamıyla entegre edilmesi ve istenilen görünüme kavuşturulması süreçleri oldukça zorlayıcı oldu. Modal'ların açılıp kapanması, responsive geçişler, grid sisteminin kayması gibi önyüz sorunlarında elementleri düzgün oturtmak efor ve ince ayar gerektirmiştir.

---

## Quality ve Code Review (Code Cleanup / SonarQube Raporları)

Projeyi daha profesyonel ve teknik borcu düşük bir mimari haline getirmek için **SonarQube** statik analiz araçları çalıştırılmış ve temizleme işlemleri yapılmıştır. 
- **Teknik Borç (Technical Debt)** hesaplamalarında ölü (dead-code) şablon artıkları ve kullanılmayan controller rotaları (örneğin Home/Privacy.cshtml), Program.cs altındaki statik log ve class tanımlamaları silinmiştir. 
- Yapılan güncellemeler ve gereksiz uzantıların temizlenmesi sonucu projenin kaynak kod temizliği sağlanmış, hata ve \
ull warning\ olasılıkları giderilmiş ve **teknik borç oranı %5'in altına başarılı bir şekilde düşürülmüştür.** Her adım sonrası Controller derlemeleri (build) testten geçirilmiştir.

---

## Kullanılan Yapay Zeka Araçları

Bu projede **Gemini 3.1 Pro** kullanılarak Refactoring, Code Clean-up ve Bug-Fixing gerçekleştirilmiştir.  
Kullanılan teknikler:
- Proje içindeki gereksiz/ölü kodların silinip mimarinin 3 ana tablo üzerine optimize edilmesi ("Dead Code Elimination").
- UX iyileştirmesi için JavaScript kullanılarak anlık "Input Mask" kodlanması (Prompt Engineering).
- SOLID ve Clean Architecture standartlarına uygun biçimlendirme, Türkçe hata mesajı özelleştirmeleri, "Durum/Öncelik ve Arşiv" altyapısının eklenmesi yapıldı.
- **Statik kod analizi (SonarQube) ile refactoring testleri edilip derleme aşamasındaki uyarıları giderilmesi sağlandı.**
