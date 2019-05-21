using Microsoft.EntityFrameworkCore;
using Shop.DAL.Helpers;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DAL.Implementations
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly EFDbContext _context;

        public ProductsRepository(EFDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IList<Product> GetAllProducts()
        {
            return _context.Products.Include(m => m.Category).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
