using Ecommerce_Core.DTOs;
using Ecommerce_Core.Interfaces;
using Ecommerce_Core.Models;
using Ecommerce_Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Infrastracture.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly AppDbContext _context;

        public ReviewRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews.Include(r => r.Product).Include(r => r.User).ToListAsync();

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _context.Reviews.Include(r => r.Product).Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return null;

            return new ReviewDto
            {
                Id = review.Id,
                ProductId = review.ProductId,
                UserId = review.UserId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _context.Reviews.Include(r => r.User)
                .Where(r => r.ProductId == productId).ToListAsync();

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(int userId)
        {
            var reviews = await _context.Reviews.Include(r => r.Product)
                .Where(r => r.UserId == userId).ToListAsync();

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }

        public async Task<string> AddReviewAsync(ReviewDto reviewDto)
        {
            var review = new Review
            {
                ProductId = reviewDto.ProductId,
                UserId = reviewDto.UserId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return "Review added successfully";
        }

        public async Task<string> UpdateReviewAsync(int id, ReviewDto reviewDto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return "Review not found";

            review.Rating = reviewDto.Rating;
            review.Comment = reviewDto.Comment;

            await _context.SaveChangesAsync();

            return "Review updated successfully";
        }

        public async Task<string> DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return "Review not found";

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return "Review deleted successfully";
        }
    }
}
