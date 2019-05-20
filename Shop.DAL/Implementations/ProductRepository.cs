using Shop.DAL.Helpers;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;

namespace Shop.DAL.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly EFDbContext _context;

        public ProductRepository(EFDbContext context)
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
    }
}
