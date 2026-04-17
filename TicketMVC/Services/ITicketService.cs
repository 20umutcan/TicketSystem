using TicketMVC.Models;

namespace TicketMVC.Services
{
    public interface ITicketService
    {
        // Categories
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int id);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);

        // Products
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        // WarrantyParts
        Task<IEnumerable<WarrantyPart>> GetWarrantyPartsByProductAsync(int productId);
        Task CreateWarrantyPartAsync(WarrantyPart part);
        Task UpdateWarrantyPartAsync(WarrantyPart part);
        Task DeleteWarrantyPartAsync(int id);

        // Tickets
        Task<IEnumerable<Ticket>> GetTicketsAsync();
        Task<IEnumerable<Ticket>> GetArchivedTicketsAsync();
        Task<Ticket?> GetTicketAsync(int id);
        Task CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int id);
    }
}
