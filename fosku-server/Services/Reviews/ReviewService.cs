using fosku_server.Data;
using fosku_server.Models;

namespace fosku_server.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public Review GetReview(int id)
        {
            var query = from obj in _context.Reviews
                        where obj.Id == id
                        select obj;
            Review? Obj = query.FirstOrDefault();
            return Obj;
        }

        public void CreateReview(Review review)
        {
            _context.Reviews.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
        }
    }
}
