namespace NomuBackend.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<bool> CreateProductAsync(Product product); // Changed to Task<bool>
        Task<bool> UpdateProductAsync(string id, Product productIn); // Changed to Task<bool>
        Task<bool> RemoveProductAsync(string id); // Changed to Task<bool>
    }
}
