using Shop.DAL.Helpers;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.DAL.Implementations
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly EFDbContext _context;

        public CategoriesRepository(EFDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Remove(category);
        }

        public IList<Category> GetAllCategories()
        {
            return _context.Categories.ToList() ;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(m => m.Id == id);
        }

        public Category GetCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(m => m.Name == name);
        }

        public void Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }
    }
}
