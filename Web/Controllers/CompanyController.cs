﻿using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDTO body)
        {
            var response = await _companyService.Add(body);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 10)
        {
            var response = await _companyService.GetAll(page, pageSize);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetByTaxIdentifier/{taxIdentifier}")]
        public async Task<IActionResult> GetByTaxIdentifier([FromRoute] string taxIdentifier)
        {
            var response = await _companyService.GetByTaxIdentifier(taxIdentifier);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
