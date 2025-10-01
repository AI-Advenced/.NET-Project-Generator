using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using ECommerceAPI.Models;
using ECommerceAPI.Services;

namespace ECommerceAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/orderitem")]
    public class OrderItemController : ApiController
    {
        private readonly IOrderItemService _orderitemService;

        public OrderItemController(IOrderItemService orderitemService)
        {
            _orderitemService = orderitemService;
        }

        // GET api/orderitem
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var orderitems = _orderitemService.GetAll();
                return Ok(orderitems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/orderitem/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var orderitem = _orderitemService.GetById(id);
                if (orderitem == null)
                    return NotFound();

                return Ok(orderitem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/orderitem
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]OrderItem orderitem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdOrderItem = _orderitemService.Create(orderitem);
                return Created($"api/orderitem/{createdOrderItem.Id}", createdOrderItem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/orderitem/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]OrderItem orderitem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingOrderItem = _orderitemService.GetById(id);
                if (existingOrderItem == null)
                    return NotFound();

                var updatedOrderItem = _orderitemService.Update(orderitem);
                return Ok(updatedOrderItem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/orderitem/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _orderitemService.Delete(id);
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