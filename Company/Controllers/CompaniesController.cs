using Company.Commands.AddCompany;
using Company.Commands.UpdateCommand;
using Company.Queries.GetAllCompanies;
using Company.Queries.GetCompanyById;
using Company.Queries.GetCompanyByIsin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(IMediator mediator, ILogger<CompaniesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyCommand command)
        {
            _logger.LogInformation("Create Company Request ",  JsonSerializer.Serialize(command) );
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCompanyById), new { id }, null);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateCompany( [FromBody] UpdateCompanyCommand company)
        {
            _logger.LogInformation("Received request to update company with ID: {CompanyId}", JsonSerializer.Serialize(company));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for company update request.");
                return BadRequest(ModelState);
            }

            var command = new UpdateCompanyCommand
            {
                Id = company.Id,
                Name = company.Name,
                Ticker = company.Ticker,
                Exchange = company.Exchange,
                Isin = company.Isin,
                WebsiteUrl = company.WebsiteUrl
            };

            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound($"Company with ID {company.Id} not found.");
            }

            return NoContent();
        }
        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAll()
        {
            var company = await _mediator.Send(new GetAllCompaniesQuery());
            return company != null ? Ok(company) : NotFound();
        }


        [HttpGet("CompanyID/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _mediator.Send(new GetCompanyByIdQuery { Id = id });
            return company != null ? Ok(company) : NotFound();
        }
        [HttpGet("CompanyByInIs/{InIs}")]
        public async Task<IActionResult> GetCompanyByISIN(string InIs)
        {
            var company = await _mediator.Send(new GetCompanyByIsinQuery { InIs = InIs });
            return company != null ? Ok(company) : NotFound();
        }

    }
}
