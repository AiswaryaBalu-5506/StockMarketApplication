using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SectorMicroService.Repository;

namespace SectorMicroService.Controllers
{
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

        /*
        // POST: api/Sector
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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
}
