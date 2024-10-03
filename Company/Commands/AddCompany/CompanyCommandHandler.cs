using Company.Interface;
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
                StockTicker = request.StockTicker,
                Exchange = request.Exchange,
                Isin = request.Isin,
                WebsiteUrl = request.WebsiteUrl
            };
            return await _companyRepository.CreateCompanyAsync(company);
        }
    }
}
