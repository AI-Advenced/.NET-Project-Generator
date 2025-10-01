using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BlogAPI.Models;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/author")]
    public class AuthorController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET api/author
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var authors = _authorService.GetAll();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/author/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var author = _authorService.GetById(id);
                if (author == null)
                    return NotFound();

                return Ok(author);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/author
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdAuthor = _authorService.Create(author);
                return Created($"api/author/{createdAuthor.Id}", createdAuthor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/author/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingAuthor = _authorService.GetById(id);
                if (existingAuthor == null)
                    return NotFound();

                var updatedAuthor = _authorService.Update(author);
                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/author/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _authorService.Delete(id);
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