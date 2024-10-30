using fosku_server.Data;
using fosku_server.Models;

namespace fosku_server.Services.ProductImages
{
    public class ProductImageService : IProductImageService
    {
        private readonly AppDbContext _context;

        public ProductImageService(AppDbContext context)
        {
            _context = context;
        }

        public ProductImage? GetProductImage(int id)
        {
            var query = from obj in _context.ProductImages
                        where obj.Id == id
                        select obj;
            ProductImage? Obj = query.FirstOrDefault();
            return Obj;
        }

        public void CreateProductImage(ProductImage image)
        {
            _context.ProductImages.Add(image);
        }

        public void UpdateProductImage(ProductImage image) 
        {
            _context.ProductImages.Update(image);
        }
    }
}
