using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyMicroService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repo;
        public CompanyController(ICompanyRepository repo)
        {
            _repo = repo;
        }


        // GET: api/Company
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.getCompanyDetails());
        }

        // GET: api/Company/IPO
        [HttpGet]
        [Route("IPO/{Companyid}")]
        public IActionResult Get(int Companyid)
        {
           return Ok(_repo.getCompanyIPODetails(Companyid));
        }

        // GET: api/Company/StockPrice
        [HttpGet]
        [Route("StockPrice/{Companyid}")]
        public IActionResult GetStockPrice(int Companyid)
        {
            return Ok(_repo.getCompanyStockPrice(Companyid));
        }

        // GET: api/Company/SearchCompanies/SearchStr
        [HttpGet]
        [Route("SearchCompanies/{searchStr}")]
        public IActionResult GetSearchCompanies(string searchStr)
        {
            return Ok(_repo.getMatchingCompanies(searchStr));
        }

        /*
        // POST: api/Company
        [HttpGet]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
