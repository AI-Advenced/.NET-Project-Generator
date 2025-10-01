using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using InventoryAPI.Models;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/supplier")]
    public class SupplierController : ApiController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET api/supplier
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var suppliers = _supplierService.GetAll();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/supplier/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var supplier = _supplierService.GetById(id);
                if (supplier == null)
                    return NotFound();

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/supplier
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdSupplier = _supplierService.Create(supplier);
                return Created($"api/supplier/{createdSupplier.Id}", createdSupplier);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/supplier/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingSupplier = _supplierService.GetById(id);
                if (existingSupplier == null)
                    return NotFound();

                var updatedSupplier = _supplierService.Update(supplier);
                return Ok(updatedSupplier);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/supplier/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _supplierService.Delete(id);
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