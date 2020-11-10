using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockExchangeMicroService.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockExchangeMicroService.Controllers
{
    [Route("api/[controller]")]
    public class StockExchangeController : Controller
    {
        private readonly IExchangeRepository _repo;
        public StockExchangeController(IExchangeRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.getStockEchanges());
        }

        // GET api/StockExchange/Companies
        [HttpGet]
        [Route("Companies/{ExchangeID}")]
        public IActionResult Get(int ExchangeID)
        {
            return Ok(_repo.getCompaniesInAExchange(ExchangeID));
        }

        /*
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
