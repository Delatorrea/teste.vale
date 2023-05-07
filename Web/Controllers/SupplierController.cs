using Domain.PurchaseContext.DTOs;
using Domain.PurchaseContext.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
        {
            _logger = logger;
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierDTO body)
        {
            var response = await _supplierService.Add(body);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 10)
        {
            var response = await _supplierService.GetAll(page, pageSize);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _supplierService.GetById(id);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetByTaxIdentifier/{taxIdentifier}")]
        public async Task<IActionResult> GetByTaxIdentifier([FromRoute] string taxIdentifier)
        {
            var response = await _supplierService.GetByTaxIdentifier(taxIdentifier);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] SupplierDTO body)
        {
            var response = await _supplierService.Update(id, body);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _supplierService.Delete(id);
            if (!response.IsValid())
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
