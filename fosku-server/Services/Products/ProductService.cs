using fosku_server.Data;
using fosku_server.Models;
using Microsoft.EntityFrameworkCore;

namespace fosku_server.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Product? GetProduct(int id)
        {
            var query = from obj in _context.Products
                        where obj.Id == id
                        select obj;
            Product? Obj = query.FirstOrDefault();
            return Obj;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChangesAsync();
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChangesAsync();
        }

        public void DeleteProductById(int id)
        {
            Product? product = GetProduct(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChangesAsync();
            }
        }
    }
}
