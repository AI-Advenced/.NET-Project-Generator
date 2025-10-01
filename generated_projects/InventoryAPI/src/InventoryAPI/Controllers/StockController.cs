using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using InventoryAPI.Models;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/stock")]
    public class StockController : ApiController
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        // GET api/stock
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var stocks = _stockService.GetAll();
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET api/stock/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var stock = _stockService.GetById(id);
                if (stock == null)
                    return NotFound();

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST api/stock
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Stock stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdStock = _stockService.Create(stock);
                return Created($"api/stock/{createdStock.Id}", createdStock);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/stock/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]Stock stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingStock = _stockService.GetById(id);
                if (existingStock == null)
                    return NotFound();

                var updatedStock = _stockService.Update(stock);
                return Ok(updatedStock);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/stock/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _stockService.Delete(id);
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