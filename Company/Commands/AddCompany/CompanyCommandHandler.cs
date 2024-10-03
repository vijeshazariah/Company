using Company.Interface;
using Company.Middleware;
using MediatR;
using Comp = Company.Model.Company;
namespace Company.Commands.AddCompany

{
    public class CompanyCommandHandler : IRequestHandler<CompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<int> Handle(CompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Comp
            {
                Name = request.Name,
                Ticker = request.Ticker,
                Exchange = request.Exchange,
                Isin = request.Isin,
                WebsiteUrl = request.WebsiteUrl
            };
            if (_companyRepository.ValidateIsin(company.Isin))
                return await _companyRepository.CreateCompanyAsync(company);
            else throw new BusinessValidationException($"A company with ISIN {company.Isin} already exists.");
        }
    }
}
