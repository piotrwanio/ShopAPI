using Shop.BLL.Models;
using System.Collections.Generic;

namespace Shop.BLL.Interfaces
{
    public interface IProductsService
    {
        void AddProduct(ProductDTO productDTO);
        IList<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int id);
        void UpdateProduct(ProductDTO product);
        void DeleteProductById(int id);
        IList<ProductDTO> GetProductsByCategory(int categoryId);
    }
}