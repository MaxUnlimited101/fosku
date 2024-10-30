using fosku_server.Models;

namespace fosku_server.Services.ProductImages
{
    public interface IProductImageService
    {
        public ProductImage? GetProductImage(int id);
        public void CreateProductImage(ProductImage image);
        public void UpdateProductImage(ProductImage image);
    }
}
