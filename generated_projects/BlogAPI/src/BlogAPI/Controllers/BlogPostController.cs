using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BlogAPI.Models;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/blogpost")]
    public class BlogPostController : ApiController
    {
        private readonly IBlogPostService _blogpostService;

        public BlogPostController(IBlogPostService blogpostService)
        {
            _blogpostService = blogpostService;
        }

        // GET api/blogpost
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var blogposts = _blogpostService.GetAll();
                return Ok(blogposts);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/blogpost/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var blogpost = _blogpostService.GetById(id);
                if (blogpost == null)
                    return NotFound();

                return Ok(blogpost);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/blogpost
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]BlogPost blogpost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdBlogPost = _blogpostService.Create(blogpost);
                return Created($"api/blogpost/{createdBlogPost.Id}", createdBlogPost);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/blogpost/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]BlogPost blogpost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingBlogPost = _blogpostService.GetById(id);
                if (existingBlogPost == null)
                    return NotFound();

                var updatedBlogPost = _blogpostService.Update(blogpost);
                return Ok(updatedBlogPost);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/blogpost/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _blogpostService.Delete(id);
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