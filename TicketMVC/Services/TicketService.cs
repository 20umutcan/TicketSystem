using System.Net.Http.Json;
using TicketMVC.Models;

namespace TicketMVC.Services
{
    public class TicketService : ITicketService
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl;

        public TicketService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5062/api/";
        }

        // Categories
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Category>>(_baseUrl + "categories") ?? new List<Category>();

        public async Task<Category?> GetCategoryAsync(int id)
            => await _httpClient.GetFromJsonAsync<Category>(_baseUrl + $"categories/{id}");

        public async Task CreateCategoryAsync(Category category)
            => (await _httpClient.PostAsJsonAsync(_baseUrl + "categories", category)).EnsureSuccessStatusCode();

        public async Task UpdateCategoryAsync(Category category)
            => (await _httpClient.PutAsJsonAsync(_baseUrl + $"categories/{category.Id}", category)).EnsureSuccessStatusCode();

        public async Task DeleteCategoryAsync(int id)
            => (await _httpClient.DeleteAsync(_baseUrl + $"categories/{id}")).EnsureSuccessStatusCode();

        // Products
        public async Task<IEnumerable<Product>> GetProductsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Product>>(_baseUrl + "products") ?? new List<Product>();

        public async Task<Product?> GetProductAsync(int id)
            => await _httpClient.GetFromJsonAsync<Product>(_baseUrl + $"products/{id}");

        public async Task CreateProductAsync(Product product)
            => (await _httpClient.PostAsJsonAsync(_baseUrl + "products", product)).EnsureSuccessStatusCode();

        public async Task UpdateProductAsync(Product product)
            => (await _httpClient.PutAsJsonAsync(_baseUrl + $"products/{product.Id}", product)).EnsureSuccessStatusCode();

        public async Task DeleteProductAsync(int id)
            => (await _httpClient.DeleteAsync(_baseUrl + $"products/{id}")).EnsureSuccessStatusCode();

        // Tickets
        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Ticket>>(_baseUrl + "tickets") ?? new List<Ticket>();

        public async Task<IEnumerable<Ticket>> GetArchivedTicketsAsync()
            => await _httpClient.GetFromJsonAsync<IEnumerable<Ticket>>(_baseUrl + "tickets/archived") ?? new List<Ticket>();

        public async Task<Ticket?> GetTicketAsync(int id)
            => await _httpClient.GetFromJsonAsync<Ticket>(_baseUrl + $"tickets/{id}");

        public async Task CreateTicketAsync(Ticket ticket)
            => (await _httpClient.PostAsJsonAsync(_baseUrl + "tickets", ticket)).EnsureSuccessStatusCode();

        public async Task UpdateTicketAsync(Ticket ticket)
            => (await _httpClient.PutAsJsonAsync(_baseUrl + $"tickets/{ticket.Id}", ticket)).EnsureSuccessStatusCode();

        public async Task DeleteTicketAsync(int id)
            => (await _httpClient.DeleteAsync(_baseUrl + $"tickets/{id}")).EnsureSuccessStatusCode();
    }
}

