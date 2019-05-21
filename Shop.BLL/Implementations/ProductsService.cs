using AutoMapper;
using Shop.BLL.Helpers;
using Shop.BLL.Interfaces;
using Shop.BLL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.BLL.Implementations
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository productRepository, ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public void AddProduct(ProductDTO productDTO)
        {
            if (_categoryRepository.GetCategoryByName(productDTO.Category.Name) == null
                || _categoryRepository.GetCategoryById(productDTO.Category.Id) == null)
                throw new ValidationException("Product's category doesn't exist.");
  

            Product product = _mapper.Map<Product>(productDTO);

            _productRepository.Add(product);
        }

        public void DeleteProductById(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
                throw new ValidationException("This product doesn't exist");

            _productRepository.Delete(product);
        }

        public IList<ProductDTO> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            if (products == null)
                throw new ValidationException("There was no products");

            foreach (var product in products.ToList())
            {
                productDTOs.Add(_mapper.Map<ProductDTO>(product));
            }

            return productDTOs;
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
                throw new ValidationException("There was no product with that id");
            else
                return _mapper.Map<ProductDTO>(product);
        }

        public IList<ProductDTO> GetProductsByCategory(int categoryId)
        {
            var products = _productRepository.GetAllProducts()
                .Where(p => p.CategoryId == categoryId);
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            if (products == null)
                throw new ValidationException("There is no products in this category");

            foreach(var p in products)
            {
                productDTOs.Add(_mapper.Map<ProductDTO>(p));
            }

            return productDTOs;
        }

        public void UpdateProduct(ProductDTO product)
        {
            try
            {
                ProductValidation(product);
                _productRepository.Update(_mapper.Map<Product>(product));
            }
            catch (ValidationException e)
            {
                throw e;
            }
        }

        private void ProductValidation(ProductDTO product)
        {
            //check if received category exist in database
            if (_categoryRepository.GetCategoryByName(product.Category.Name) == null)
                throw new ValidationException("Product's category doesn't exist.");

            //check if product exist in database
            if (_productRepository.GetProductById(product.Id) == null)
                throw new ValidationException("Product doesn't exist.");

            //empty properties validation
            if (product.Name == null)
                throw new ValidationException("Product name is required");
            if (product.Price == 0)
                throw new ValidationException("Product price is required");
            if (product.Unit == null)
                throw new ValidationException("Product unit is required");
            if (product.Description == null)
                throw new ValidationException("Product description is required");
            if (product.Category == null)
                throw new ValidationException("Product category is required");
        }
    }
}
