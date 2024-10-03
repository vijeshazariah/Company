using Company.Interface;
using MediatR;
using CompanyDto = Company.DAL.DTO.CompanyDTO;


namespace Company.Queries.GetAllCompanies
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository;
        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task <List<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var allCompanies = await _companyRepository.GetAllCompanies();

            var companyDtos = allCompanies.Select(c => new CompanyDto
            {
                Id = c.Id,
                Name = c.Name,
                StockTicker = c.StockTicker,
                Exchange = c.Exchange,
                Isin = c.Isin,
                WebsiteUrl = c.WebsiteUrl
            }).ToList();

            return companyDtos;
        }
    }
}
