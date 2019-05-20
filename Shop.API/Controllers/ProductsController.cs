using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using Shop.BLL.Models;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // POST api/products
        [HttpPost]
        public ActionResult Add([FromBody] ProductDTO productDTO)
        {
            try
            {
                _productService.AddProduct(productDTO);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return (List<ProductDTO>)products;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
