using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SectorMicroService.Repository;
using StockMarketWebService.Models;

namespace SectorMicroService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorRepository _repo;
        public SectorController(ISectorRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Sector
        [HttpGet]
        public IActionResult Get()
        {
           return Ok(_repo.getSectors());
        }

        // GET: api/Sector/5
        [HttpGet]
        [Route("Companies/{SectorID}")]
        public IActionResult GetCompaniesInSector(int SectorID)
        {
            return Ok(_repo.getCompaniesInSector(SectorID));
        }

        
        // POST: api/Sector
        [HttpPost]
        public IActionResult Post([FromBody] Sectors sector)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string role = identity.FindFirst("Role").Value;
            if (role == "Admin")
            {
                var res = _repo.addSector(sector);
                if (res) return Ok(new Response { StatusCode = "Success", Message = "Sector Created Successfully" });
                else return BadRequest(new Response { StatusCode = "Failed", Message = "Sector Creation Unsuccessful" });
            }
            else
            {
                return Unauthorized(new Response { StatusCode = "Failed", 
                                                   Message = "Sector Creation Unsuccessful. Only Admins can be allowed" });
            }                
        }

        /*
        // PUT: api/Sector/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        } */
    }

    internal class Response
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
