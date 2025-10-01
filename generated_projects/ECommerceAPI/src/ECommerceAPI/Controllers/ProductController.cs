using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using ECommerceAPI.Models;
using ECommerceAPI.Services;

namespace ECommerceAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/product
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var products = _productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/product/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var product = _productService.GetById(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/product
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdProduct = _productService.Create(product);
                return Created($"api/product/{createdProduct.Id}", createdProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/product/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingProduct = _productService.GetById(id);
                if (existingProduct == null)
                    return NotFound();

                var updatedProduct = _productService.Update(product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/product/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _productService.Delete(id);
                if (!result)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}