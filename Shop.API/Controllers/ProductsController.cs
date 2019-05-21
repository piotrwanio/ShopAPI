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
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Adds new product.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST api/products
        ///     {
        ///        "Name": "TestProduct",
        ///        "Description": "LoremipsumLoremipsumLoremipsumLoremipsumLoremipsumLoremipsumLoremipsumLoremipsum",
        ///        "Price" : 100,
        ///        "Unit" : "kg",
        ///        "Quantity" : 10,
        ///        "Category" : {
        ///           "Id" : 1,
        ///           "Name" : "Books"
        ///        }
        ///     }
        ///
        /// </remarks>
        // POST api/products
        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                _productService.AddProduct(productDTO);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all products or all products in specified category.
        /// </summary>
        /// <param name="category"></param>  
        // GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAllProducts(int category)
        {
            IEnumerable<ProductDTO> productDTOs;
            try
            {
                if (category != 0)
                {
                    productDTOs = _productService.GetProductsByCategory(category);
                }
                else
                {
                    productDTOs = _productService.GetAllProducts();
                }
                return productDTOs.ToList();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a specific product.
        /// </summary>
        /// <param name="id"></param>  
        // GET api/products/1
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                return product;
            }
            catch(ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates a specific product.
        /// </summary>
        /// <param name="id"></param>  
        // PUT api/products/1
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            try
            {
                _productService.UpdateProduct(product);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a specific product.
        /// </summary>
        /// <param name="id"></param>  
        // DELETE api/products/1
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProductById(id);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
