using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;

        public ReviewsController(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet("get-all-reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync();
            if (!reviews.Any()) return NoContent();

            return Ok(reviews);
        }

        [HttpGet("get-review-by-id/{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewRepo.GetReviewByIdAsync(id);
            if (review == null) return NotFound("Review not found");

            return Ok(review);
        }

        [HttpGet("get-reviews-by-product-id/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewRepo.GetReviewsByProductIdAsync(productId);
            if (!reviews.Any()) return NoContent();

            return Ok(reviews);
        }

        [HttpGet("get-reviews-by-user-id/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var reviews = await _reviewRepo.GetReviewsByUserIdAsync(userId);
            if (!reviews.Any()) return NoContent();

            return Ok(reviews);
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            var result = await _reviewRepo.AddReviewAsync(reviewDto);
            return Ok(result);
        }

        [HttpPut("update-review/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewDto)
        {
            var result = await _reviewRepo.UpdateReviewAsync(id, reviewDto);
            if (result == "Review not found") return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("delete-review/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewRepo.DeleteReviewAsync(id);
            if (result == "Review not found") return NotFound(result);

            return Ok(result);
        }
    }
}
