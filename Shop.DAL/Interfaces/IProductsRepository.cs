using Shop.DAL.Models;
using System.Collections.Generic;

namespace Shop.DAL.Interfaces
{
    public interface IProductsRepository
    {
        void Add(Product product);
        IList<Product> GetAllProducts();
        Product GetProductById(int id);
        void Delete(Product product);
        void Update(Product product);
    }
}