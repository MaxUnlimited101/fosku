using fosku_server.Models;

namespace fosku_server.Services.Categories
{
    public interface ICategoryService
    {
        public void CreateCategory(Category category);
        public Category GetCategory(int id);
        public void UpdateCategory(Category category);
    }
}
