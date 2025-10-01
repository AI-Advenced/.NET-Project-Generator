using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BlogAPI.Models;
using BlogAPI.Services;

namespace BlogAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/comment")]
    public class CommentController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET api/comment
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var comments = _commentService.GetAll();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/comment/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var comment = _commentService.GetById(id);
                if (comment == null)
                    return NotFound();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/comment
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdComment = _commentService.Create(comment);
                return Created($"api/comment/{createdComment.Id}", createdComment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/comment/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingComment = _commentService.GetById(id);
                if (existingComment == null)
                    return NotFound();

                var updatedComment = _commentService.Update(comment);
                return Ok(updatedComment);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/comment/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _commentService.Delete(id);
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