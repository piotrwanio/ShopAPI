using Shop.DAL.Models;

namespace Shop.DAL.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
    }
}