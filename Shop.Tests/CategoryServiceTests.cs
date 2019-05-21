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
using System.Linq;

namespace BLL.Tests
{
    public class CategoryServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCategory_ValidCategory_Success()
        {
            //arrange
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name  = "Testowa"
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<Category>(categoryDTO)).Returns(category);
            mockRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns((Category)null);

            //act
            service.AddCategory(categoryDTO);

            //asserts
            mockRepo.Verify(m => m.Add(category));
        }


        [Test]
        public void AddCategory_DuplicateCategory_Unsuccess()
        {
            //setup mocks
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name  = "Testowa"
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<CategoryDTO, Category>(categoryDTO)).Returns(category);

            service.AddCategory(categoryDTO);
            mockRepo.Setup(m => m.GetCategoryByName(categoryDTO.Name)).Returns(category);

            //act and asserts
            Assert.Throws<ValidationException>(() => {
                service.AddCategory(categoryDTO);
            });
        }

        [Test]
        public void DeleteCategory_ValidCategory_Success()
        {
            //setup mocks
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Id = 1,
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Id = 1,
                Name = "Testowa"
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<CategoryDTO, Category>(categoryDTO)).Returns(category);
            mockRepo.Setup(m => m.GetCategoryById(categoryDTO.Id)).Returns(category);

            //act
            service.DeleteCategoryById(categoryDTO.Id);

            //asserts
            mockRepo.Verify(m => m.Delete(category));
        }

        [Test]
        public void DeleteCategory_NotExistingCategory_Unsuccess()
        {
            //setup mocks
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Id = 1,
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Id = 1,
                Name = "Testowa"
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<CategoryDTO, Category>(categoryDTO)).Returns(category);
            mockRepo.Setup(m => m.GetCategoryById(categoryDTO.Id)).Returns((Category)null);

            //act and asserts
            Assert.Throws<ValidationException>(() => {
                service.DeleteCategoryById(categoryDTO.Id);
            });
        }

        [Test]
        public void GetAllCategories_NoParams_GetListOfCategories()
        {
            //arrange
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "Testowa"
            };
            List<Category> categories = new List<Category>
            {
                category
            };
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>
            {
                categoryDTO
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDTO);
            mockRepo.Setup(m => m.GetAllCategories()).Returns(categories);

            //act
            var result = service.GetAllCategories().ToList();

            //asserts
            Assert.AreEqual(result, categoryDTOs);
        }

        [Test]
        public void GetAllCategories_NoCategories_ThrowException()
        {
            //arrange
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "Testowa"
            };
            List<Category> categories = new List<Category>
            {
                category
            };
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>
            {
                categoryDTO
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDTO);
            mockRepo.Setup(m => m.GetAllCategories()).Returns((List<Category>)null);

            //act and asserts
            Assert.Throws<ValidationException>(() => {
                service.GetAllCategories();
            });
        }

        [Test]
        public void UpdateCategory_ValidCategory_Success()
        {
            //arrange
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "Testowa"
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<Category>(categoryDTO)).Returns(category);
            mockRepo.Setup(m => m.GetCategoryById(categoryDTO.Id)).Returns(category);

            //act
            service.UpdateCategory(categoryDTO);

            //asserts
            mockRepo.Verify(m => m.Update(category));
        }


        [Test]
        public void UpdateCategory_NotExistingCategoryId_ThrowsValidationException()
        {
            //arrange
            Mock<ICategoriesRepository> mockRepo = new Mock<ICategoriesRepository>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();

            Category category = new Category
            {
                Name = "Testowa"
            };
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = "Testowa"
            };

            CategoriesService service = new CategoriesService(mockRepo.Object, mockMapper.Object);
            mockMapper.Setup(m => m.Map<Category>(categoryDTO)).Returns(category);
            mockRepo.Setup(m => m.GetCategoryById(categoryDTO.Id)).Returns((Category)null);

            //act and asserts
            Assert.Throws<ValidationException>(() => {
                service.UpdateCategory(categoryDTO);
            });
        }
    }
}