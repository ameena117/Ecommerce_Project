using Ecommerce_Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Core.Interfaces
{
    public interface ICategoryRepo
    {        
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> GetCategoryByNameAsync(string name);
        Task<String> AddCategoryAsync(CategoryDto categoryDto);
        Task <String> DeleteCategoryAsync(int id);
        Task<String> UpdateCategoryAsync(CategoryDto categoryDto,int id);
    }
}
