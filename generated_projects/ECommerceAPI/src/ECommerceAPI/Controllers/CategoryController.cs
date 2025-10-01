using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using ECommerceAPI.Models;
using ECommerceAPI.Services;

namespace ECommerceAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/category
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var categorys = _categoryService.GetAll();
                return Ok(categorys);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/category/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var category = _categoryService.GetById(id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/category
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdCategory = _categoryService.Create(category);
                return Created($"api/category/{createdCategory.Id}", createdCategory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/category/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingCategory = _categoryService.GetById(id);
                if (existingCategory == null)
                    return NotFound();

                var updatedCategory = _categoryService.Update(category);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/category/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _categoryService.Delete(id);
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