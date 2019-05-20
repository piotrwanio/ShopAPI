using AutoMapper;
using Shop.BLL.Interfaces;
using Shop.BLL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;
using System.Collections.Generic;

namespace Shop.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public void AddProduct(ProductDTO productDTO)
        {
            if(string.IsNullOrEmpty(productDTO.Name))
            {
                throw new Exception("Product's name is necessary");
            }

            Product product = (Product)_mapper.Map(productDTO, typeof(ProductDTO), typeof(Product));

            _productRepository.Add(product);
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
