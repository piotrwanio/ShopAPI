using Shop.DAL.Models;
using System.Collections.Generic;

namespace Shop.DAL.Interfaces
{
    public interface ICategoriesRepository
    {
        void Add(Category product);
        IList<Category> GetAllCategories();
        void Delete(Category category);
        Category GetCategoryByName(string name);
        Category GetCategoryById(int id);
        void Update(Category category);
    }
}