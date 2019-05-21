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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public void AddCategory(CategoryDTO categoryDTO)
        {
            //check if this category already exists
            if (_categoryRepository.GetCategoryByName(categoryDTO.Name) != null)
                throw new ValidationException("This category name already exists");

            //map db entity to DTO
            var category = _mapper.Map<Category>(categoryDTO);

            //add category to DB
            _categoryRepository.Add(category);
        }

        public void DeleteCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            //check if category exists in db
            if (category == null)
                throw new ValidationException("This category doesn't exist");

            //delete category
            _categoryRepository.Delete(category);
        }

        public IList<CategoryDTO> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();

            //throw exception if there is no categories
            if (categories == null)
                throw new ValidationException("There is no categories");

            //map all db entities to DTOs
            foreach(var c in categories)
            {
                categoryDTOs.Add(_mapper.Map<CategoryDTO>(c));
            }

            return categoryDTOs;
        }

        public void UpdateCategory(CategoryDTO categoryDTO)
        {
            //check if category exists in db
            if (_categoryRepository.GetCategoryById(categoryDTO.Id) == null)
                throw new ValidationException("This category doesn't exists");

            //map category db entity to DTO
            var category = _mapper.Map<Category>(categoryDTO);

            //update category
            _categoryRepository.Update(category);
        }
    }
}
