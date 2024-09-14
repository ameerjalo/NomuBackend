using MongoDB.Driver;
using NomuBackend.Settings;

namespace NomuBackend.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoClient client, IMongoDbSettings settings)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        // Get all products asynchronously, returns Task<List<Product>>
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        // Get a product by ID asynchronously, returns Task<Product>
        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _products.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();
        }

        // Create a product asynchronously, returns Task<bool> to indicate success/failure
        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                await _products.InsertOneAsync(product);
                return true; // return true if the product is successfully created
            }
            catch
            {
                return false; // return false if there's an exception
            }
        }

        // Update a product asynchronously, returns Task<bool> to indicate success/failure
        public async Task<bool> UpdateProductAsync(string id, Product productIn)
        {
            var result = await _products.ReplaceOneAsync(product => product.Id == id, productIn);
            return result.IsAcknowledged && result.ModifiedCount > 0; // return true if update was successful
        }

        // Remove a product asynchronously, returns Task<bool> to indicate success/failure
        public async Task<bool> RemoveProductAsync(string id)
        {
            var result = await _products.DeleteOneAsync(product => product.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0; // return true if removal was successful
        }
    }
}
