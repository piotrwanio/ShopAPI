using AutoMapper;
using Moq;
using NUnit.Framework;
using Shop.BLL.Implementations;
using Shop.BLL.Models;
using Shop.DAL.Interfaces;
using Shop.DAL.Models;
using System;

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
            //setup mocks
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };
            ProductDTO productDTO = new ProductDTO
            {
                Name  = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };

            ProductService service = new ProductService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map(productDTO, typeof(ProductDTO), typeof(Product))).Returns(product);

            //act
            service.AddProduct(productDTO);

            //aserts
            mockRepo.Verify(m => m.Add(product));
        }

        [Test]
        public void AddProduct_WithoutName_ThrowsNoNameException()
        {
            //setup mocks
            Mock<IProductRepository> mockRepo = new Mock<IProductRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            ProductDTO productDTO = new ProductDTO
            {
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };

            ProductService service = new ProductService(mockRepo.Object, mockMapper.Object);

            //act and aserts
            Assert.Throws<Exception>(() => { service.AddProduct(productDTO); });
        }
    }
}