using Company.Interface;
using Company.Middleware;
using Company.Model;
using Company.Queries.GetCompanyById;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Company.Commands.UpdateCommand
{
    public class UpdateCompanyCommandHandler: IRequestHandler<UpdateCompanyCommand, bool>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        private readonly IMediator _mediator;  

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, ILogger<UpdateCompanyCommandHandler> logger, IMediator mediator)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateCompanyCommand for company ID: {CompanyId}", request.Id);

             var existingCompany = await _mediator.Send(new GetCompanyByIdQuery { Id = request.Id });
            if (existingCompany == null)
            {
                _logger.LogWarning("Company with ID {CompanyId} not found.", request.Id);
                return false;
            }

            // Update the company entity
            var companyDataToUpdate = new Model.Company() 
            {
                Id = request.Id,
                Name = request.Name,
                Ticker = request.Ticker,
                Exchange = request.Exchange,
                Isin = request.Isin,
                WebsiteUrl = request.WebsiteUrl 
            };

            // Update the company in the database 
                await _companyRepository.UpdateCompanyAsync(companyDataToUpdate);
            _logger.LogInformation("Successfully updated company with ID: {CompanyId}", request.Id);
            return true;
        }
    }
}
