using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Repositories.Interface;
using MeuPonto.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPonto.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _companyService.GetAllCompaniesAsync();
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompanyByIdAsync(long id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> GetCompanyByCnpjAsync(string cnpj)
        {
            var company = await _companyService.GetCompanyByCnpjAsync(cnpj);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyRequestDto company)
        {
            var createdCompany = await _companyService.CreateCompanyAsync(company);
            if(createdCompany == null)
            {
                return BadRequest("Company could not be created.");
            }

            return CreatedAtAction("GetCompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompanyAsync(long id, [FromBody] CompanyRequestDto company)
        {
            try
            {
                var result = await _companyService.UpdateCompanyAsync(id, company);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyAsync(long id)
        {
            try
            {
                var result = await _companyService.DeleteCompanyAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
