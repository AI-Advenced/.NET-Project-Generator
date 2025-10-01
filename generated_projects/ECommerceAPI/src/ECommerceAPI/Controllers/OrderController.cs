using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using ECommerceAPI.Models;
using ECommerceAPI.Services;

namespace ECommerceAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/order
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var orders = _orderService.GetAll();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/order/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var order = _orderService.GetById(id);
                if (order == null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/order
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdOrder = _orderService.Create(order);
                return Created($"api/order/{createdOrder.Id}", createdOrder);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/order/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingOrder = _orderService.GetById(id);
                if (existingOrder == null)
                    return NotFound();

                var updatedOrder = _orderService.Update(order);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/order/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _orderService.Delete(id);
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