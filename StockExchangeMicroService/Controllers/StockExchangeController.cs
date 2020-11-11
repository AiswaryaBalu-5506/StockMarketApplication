using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using StockExchangeMicroService.Repositories;
using StockMarketWebService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockExchangeMicroService.Controllers
{
    [Authorize]
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

        
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] StockExchange ex)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string role = identity.FindFirst("Role").Value;
            if (role == "Admin")
            {
                var res = _repo.addNewExchange(ex);
                if (res) return Ok(new Response { StatusCode = "Success", Message = "Exchange added successfully" });
                else return BadRequest(new Response { StatusCode = "Failed", Message = "Exchange adding unsuccessful" });
            }
            else
            {
                return Unauthorized(new Response
                {
                    StatusCode = "Failed",
                    Message = "Exchange Creation Unsuccessful. Only Admins can be allowed"
                });
            }

        }


        /*
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

    internal class Response
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
