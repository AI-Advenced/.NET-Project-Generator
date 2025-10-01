using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using InventoryAPI.Models;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET api/item
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var items = _itemService.GetAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/item/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var item = _itemService.GetById(id);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/item
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdItem = _itemService.Create(item);
                return Created($"api/item/{createdItem.Id}", createdItem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/item/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingItem = _itemService.GetById(id);
                if (existingItem == null)
                    return NotFound();

                var updatedItem = _itemService.Update(item);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/item/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _itemService.Delete(id);
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