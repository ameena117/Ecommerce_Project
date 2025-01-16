using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce_Core.DTOs;
using global::Ecommerce_Core.DTOs;


namespace Ecommerce_Core.Interfaces
{
    
    
    public interface IReviewRepo
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(int productId);
        Task<IEnumerable<ReviewDto>> GetReviewsByUserIdAsync(int userId);
        Task<string> AddReviewAsync(ReviewDto reviewDto);
        Task<string> UpdateReviewAsync(int id, ReviewDto reviewDto);
        Task<string> DeleteReviewAsync(int id);
    }
}

