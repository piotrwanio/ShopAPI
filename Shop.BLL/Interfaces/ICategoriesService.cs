using Shop.BLL.Models;
using System.Collections.Generic;

namespace Shop.BLL.Interfaces
{
    public interface ICategoriesService
    {
        void AddCategory(CategoryDTO categoryDTO);
        IList<CategoryDTO> GetAllCategories();
        void UpdateCategory(CategoryDTO categpry);
        void DeleteCategoryById(int id);
    }
}