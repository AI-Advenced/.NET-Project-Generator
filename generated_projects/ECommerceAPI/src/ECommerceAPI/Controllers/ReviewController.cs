using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using ECommerceAPI.Models;
using ECommerceAPI.Services;

namespace ECommerceAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET api/review
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var reviews = _reviewService.GetAll();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/review/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var review = _reviewService.GetById(id);
                if (review == null)
                    return NotFound();

                return Ok(review);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/review
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdReview = _reviewService.Create(review);
                return Created($"api/review/{createdReview.Id}", createdReview);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/review/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingReview = _reviewService.GetById(id);
                if (existingReview == null)
                    return NotFound();

                var updatedReview = _reviewService.Update(review);
                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/review/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _reviewService.Delete(id);
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