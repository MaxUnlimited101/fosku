using fosku_server.Models;

namespace fosku_server.Services.Products
{
    public interface IProductService
    {
        public Product? GetProduct(int id);
        public void CreateProduct(Product product);
        public void UpdateProduct(Product product);
        public List<Product> GetProducts();
        public void DeleteProduct(Product product);
        public void DeleteProductById(int id);
    }
}
