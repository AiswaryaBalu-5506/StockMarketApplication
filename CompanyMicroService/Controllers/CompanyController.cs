using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyMicroService.Models;
using CompanyMicroService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using StockMarketWebService.Models;

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

        
        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] AddCompanyModel company)
        {
            var result = await _repo.AddCompany(company);
            if (result) return Ok(new Response { Status = "Success", Message = "Company Posted successfully" });
            else return BadRequest(new Response { Status = "Failed", Message = "Company creation unsuccessfully" });
        }

        // POST: api/Company/IPO
        [HttpPost]
        [Route("IPO")]
        public IActionResult PostIPO([FromBody] AddIPOModel ipo)
        {
            var result = _repo.addIPO(ipo);
            if (result) return Ok(new Response { Status = "Success", Message = "IPO Posted successfully" });
            else return BadRequest(new Response { Status = "Failed", Message = "IPO creation unsuccessfully" });
        }

        // POST: api/Company/StockPrice
        [HttpPost]
        [Route("StockPrice")]
        public IActionResult PostStockPrice([FromBody] AddStockPriceModel sp)
        {
            var result = _repo.addStockPrice(sp);
            if (result) return Ok(new Response { Status = "Success", Message = "Stock Price Posted successfully" });
            else return BadRequest(new Response { Status = "Failed", Message = "Stock Price creation unsuccessfully" });
        }

        /*
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

    internal class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
