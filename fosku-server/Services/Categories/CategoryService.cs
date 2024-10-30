using fosku_server.Data;
using fosku_server.Models;

namespace fosku_server.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public Category GetCategory(int id)
        {
            var categoriesQuery = from category in _context.Categories
                                  where category.Id == id
                                  select category;
            Category? Category = categoriesQuery.FirstOrDefault();
            return Category;
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
