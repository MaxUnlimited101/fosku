using fosku_server.Models;

namespace fosku_server.Services.Reviews
{
    public interface IReviewService
    {
        public Review GetReview(int id);
        public void CreateReview(Review review);
        public void UpdateReview(Review review);
    }
}
