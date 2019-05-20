using Shop.BLL.Models;
using System.Collections.Generic;

namespace Shop.BLL.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductDTO productDTO);
        IEnumerable<ProductDTO> GetAllProducts();
    }
}