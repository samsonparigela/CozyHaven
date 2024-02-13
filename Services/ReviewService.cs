using CozyHaven.Exceptions;
using CozyHaven.Interfaces;
using CozyHaven.Models;
using CozyHaven.Models.DTOs;

namespace CozyHaven.Services
{
    public class ReviewService : IReviewAdminService, IReviewCustomerService, IReviewHotelService
    {
        private readonly IRepository<int, Review> _repo;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(IRepository<int, Review> repo, ILogger<ReviewService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Review> AddReview(Review review)
        {
            review = await _repo.Add(review);
            return review;
        }

        public async Task<Review> DeleteReview(int reviewId)
        {
            var review = await _repo.Delete(reviewId);
            return review;
        }

        public async Task<Review> GetReview(int reviewId)
        {
            var review = await _repo.Get(reviewId);
            return review;
        }

        public async Task<List<Review>> GetAllReviews()
        {
            var reviews = await _repo.GetAll();
            return reviews;
        }

        public async Task<ReviewDTO> UpdateReview(ReviewDTO reviewDTO)
        {
            var review = await _repo.Get(reviewDTO.ReviewID);
            if (review != null)
            {
                review.Rating = reviewDTO.Rating;
                review.Comment = reviewDTO.Comment;
                review = await _repo.Update(review);
                return new ReviewDTO
                {
                    ReviewID = review.ReviewID,
                    Rating = review.Rating,
                    Comment = review.Comment
                };
            }
            throw new ReviewNotFoundException();
        }

        public async Task<List<Review>> GetAllReviewsByUser(int userId)
        {
            var reviews = await _repo.GetAll();
            return reviews.Where(r => r.UserID == userId).ToList();
        }

        public async Task<List<Review>> GetAllReviewsByHotel(int hotelId)
        {
            var reviews = await _repo.GetAll();
            return reviews.Where(r => r.HotelID == hotelId).ToList();
        }

        public async Task<List<Review>> GetAllReviewsByHotelForHotelOwner(int hotelId)
        {
            var reviews = await _repo.GetAll();
            return reviews.Where(r => r.HotelID == hotelId).ToList();
        }
    }
}
