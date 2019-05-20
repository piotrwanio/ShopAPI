using Shop.BLL.Models;

namespace Shop.BLL.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDTO productDTO);
    }
}