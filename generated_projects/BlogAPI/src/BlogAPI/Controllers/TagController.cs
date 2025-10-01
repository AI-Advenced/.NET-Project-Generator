using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BlogAPI.Models;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/tag")]
    public class TagController : ApiController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET api/tag
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var tags = _tagService.GetAll();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/tag/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var tag = _tagService.GetById(id);
                if (tag == null)
                    return NotFound();

                return Ok(tag);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/tag
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Tag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdTag = _tagService.Create(tag);
                return Created($"api/tag/{createdTag.Id}", createdTag);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/tag/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Tag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingTag = _tagService.GetById(id);
                if (existingTag == null)
                    return NotFound();

                var updatedTag = _tagService.Update(tag);
                return Ok(updatedTag);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/tag/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _tagService.Delete(id);
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