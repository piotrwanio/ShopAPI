using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Helpers;
using Shop.BLL.Interfaces;
using Shop.BLL.Models;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoryService;

        public CategoriesController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Adds new category.
        /// </summary>
        // POST api/categories
        [HttpPost]
        public ActionResult AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                _categoryService.AddCategory(categoryDTO);
                return Created("/api/categories/" + categoryDTO.Id, categoryDTO);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        // GET api/categories
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            try
            {
                var categories = _categoryService.GetAllCategories();
                return categories.ToList();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update a specific category.
        /// </summary>
        /// <param name="id"></param>  
        // PUT api/categories/1
        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, [FromBody] CategoryDTO category)
        {
            try
            {
                _categoryService.UpdateCategory(category);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a specific category.
        /// </summary>
        /// <param name="id"></param>  
        // DELETE api/categories/1
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                _categoryService.DeleteCategoryById(id);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
