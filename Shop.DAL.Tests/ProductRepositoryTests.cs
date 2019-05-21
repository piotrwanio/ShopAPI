using NUnit.Framework;
using Shop.DAL.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using Shop.DAL.Implementations;
using Shop.DAL.Models;
using System.Linq;

namespace Shop.DAL.Tests
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<EFDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<EFDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;
        }

        [Test]
        public void Add_CorrectProduct_Success()
        {
            //arrange
            EFDbContext context = new EFDbContext(_options);
            ProductsRepository repository = new ProductsRepository(context);

            Product product = new Product
            {
                Name = "Testowy",
                Description = "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsum"
            };

            //act
            repository.Add(product);

            var result = context.Products.Where(p => p.Name == "Testowy").FirstOrDefault();

            //assert
            Assert.AreEqual(result.Name, product.Name);
        }
    }
}