using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Services;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly CompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, CompanyService service)
        {
            _logger = logger;
            _companyService = service;
        }

        [HttpPost]
        public async Task <IActionResult> Post ([FromBody] CompanyDTO body)
        {
            if (body == null)
                return BadRequest();

            var response = await _companyService.GetAll();

            return Ok(response);
        }
    }
}
