using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using InventoryAPI.Models;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/warehouse")]
    public class WarehouseController : ApiController
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // GET api/warehouse
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var warehouses = _warehouseService.GetAll();
                return Ok(warehouses);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/warehouse/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var warehouse = _warehouseService.GetById(id);
                if (warehouse == null)
                    return NotFound();

                return Ok(warehouse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/warehouse
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Warehouse warehouse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdWarehouse = _warehouseService.Create(warehouse);
                return Created($"api/warehouse/{createdWarehouse.Id}", createdWarehouse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/warehouse/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Warehouse warehouse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingWarehouse = _warehouseService.GetById(id);
                if (existingWarehouse == null)
                    return NotFound();

                var updatedWarehouse = _warehouseService.Update(warehouse);
                return Ok(updatedWarehouse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/warehouse/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _warehouseService.Delete(id);
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