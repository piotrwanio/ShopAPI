using AutoMapper;
using Moq;
using NUnit.Framework;
using Shop.BLL.Helpers;
using Shop.BLL.Implementations;
using Shop.BLL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;
using System.Collections.Generic;

namespace BLL.Tests
{
    public class ProductServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddProduct_ValidProduct_Success()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Name  = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockCategoryRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns(category);
            mockCategoryRepo.Setup(m => m.GetCategoryById(categoryDTO.Id)).Returns(category);

            //act
            service.AddProduct(productDTO);

            //asserts
            mockProductRepo.Verify(m => m.Add(product));
        }

        [Test]
        public void AddProduct_ProductWithNotExistingCategory_Unsuccess()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockCategoryRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns((Category)null);

            //act and assert
            Assert.Throws<ValidationException>(() => { service.AddProduct(productDTO); });
        }

        [Test]
        public void GetAllProducts_NoParams_GetListOfProducts()
        {
            //arrange
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Product product1 = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };
            Product product2 = new Product
            {
                Name = "Testowy2",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };
            ProductDTO productDTO1 = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };
            ProductDTO productDTO2 = new ProductDTO
            {
                Name = "Testowy2",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };

            List<Product> productsList = new List<Product> { product1, product2 };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);

            mockProductRepo.Setup(m => m.GetAllProducts()).Returns(productsList);
            mockMapper.Setup(m => m.Map<ProductDTO>(product1)).Returns(productDTO1);
            mockMapper.Setup(m => m.Map<ProductDTO>(product2)).Returns(productDTO2);

            //act
            var result = service.GetAllProducts();

            //asserts
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetAllProducts_NoParamsAndNoProducts_GetNull()
        {
            //setup mocks
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);

            mockProductRepo.Setup(m => m.GetAllProducts()).Returns((List<Product>)null);

            //act and asserts
            Assert.Throws<ValidationException>(() => { service.GetAllProducts(); });
        }

        [Test]
        public void GetProductById_CorrectId_GetProduct()
        {
            //setup mocks
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Product product1 = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };
            ProductDTO productDTO1 = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);

            mockProductRepo.Setup(m => m.GetProductById(1)).Returns(product1);
            mockMapper.Setup(m => m.Map<ProductDTO>(product1)).Returns(productDTO1);

            //act
            var result = service.GetProductById(1);

            //asserts
            Assert.AreEqual(productDTO1, result);
        }

        [Test]
        public void GetProductById_NotExistingProductId_GetException()
        {
            //setup mocks
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Product product1 = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };
            ProductDTO productDTO1 = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);

            mockMapper.Setup(m => m.Map<ProductDTO>(product1)).Returns(productDTO1);

            //act and asserts
            Assert.Throws<ValidationException>(() => { service.GetProductById(1); });
        }

        [Test]
        public void UpdateProduct_ValidProduct_Success()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Price = 100,
                Quantity = 100,
                Unit = "test",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Price  = 100,
                Quantity = 100,
                Unit = "test",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockCategoryRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns(category);
            mockProductRepo.Setup(m => m.GetProductById(product.Id)).Returns(product);

            //act
            service.UpdateProduct(productDTO);

            //asserts
            mockProductRepo.Verify(m => m.Update(product));
        }

        [Test]
        public void UpdateProduct_NotExistingCategory_Unsuccess()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Price = 100,
                Quantity = 100,
                Unit = "test",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Price = 100,
                Quantity = 100,
                Unit = "test",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockCategoryRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns((Category)null);
            mockProductRepo.Setup(m => m.GetProductById(product.Id)).Returns(product);

            //act and asserts
            Assert.Throws<ValidationException>(() => { service.UpdateProduct(productDTO); });
        }

        [Test]
        public void UpdateProduct_NotExistingProduct_Unsuccess()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Price = 100,
                Quantity = 100,
                Unit = "test",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Price = 100,
                Quantity = 100,
                Unit = "test",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockCategoryRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns(category);
            mockProductRepo.Setup(m => m.GetProductById(product.Id)).Returns((Product)null);

            //act and asserts
            Assert.Throws<ValidationException>(() => { service.UpdateProduct(productDTO); });
        }

        [Test]
        public void DeleteProductById_ValidProduct_Success()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Id = 1,
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockProductRepo.Setup(m => m.GetProductById(productDTO.Id)).Returns(product);

            //act
            service.DeleteProductById(productDTO.Id);

            //asserts
            mockProductRepo.Verify(m => m.Delete(product));
        }

        [Test]
        public void DeleteProductById_InvalidProductId_ThrowsValidationException()
        {
            //arrange 
            Mock<IProductsRepository> mockProductRepo = new Mock<IProductsRepository>();
            Mock<ICategoriesRepository> mockCategoryRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "TestCategory"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "TestCategory"
            };

            Product product = new Product
            {
                Id = 1,
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = category
            };
            ProductDTO productDTO = new ProductDTO
            {
                Id = 1,
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
                Category = categoryDTO
            };

            ProductsService service = new ProductsService(mockProductRepo.Object, mockCategoryRepo.Object,
                mockMapper.Object);
            mockMapper.Setup(m => m.Map<Product>(productDTO)).Returns(product);
            mockProductRepo.Setup(m => m.GetProductById(productDTO.Id)).Returns((Product)null);

            //act and asserts
            Assert.Throws<ValidationException>(() => { service.DeleteProductById(productDTO.Id); });
        }
    }
}