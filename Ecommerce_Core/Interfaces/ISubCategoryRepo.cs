using Ecommerce_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Interfaces
{
    public interface ISubCategoryRepo
    {
        Task<List<SubCateoryDto>> GetSubCategoriesAsync();
        Task<SubCateoryDto> GetSubCategoryByIdAsync(int id);
        Task<SubCateoryDto> GetSubCategoryByNameAsync(string name);
        Task<List<SubCateoryDto>> GetSubCategoryByCatIdAsync(int catId);
        Task<String> AddSubCategoryAsync(SubCateoryDto subCateoryDto);
        Task<String> DeleteSubCategoryAsync(int id);
        Task<String> UpdateSubCategoryAsync(SubCateoryDto subCategoryDto, int id);
    }
}
