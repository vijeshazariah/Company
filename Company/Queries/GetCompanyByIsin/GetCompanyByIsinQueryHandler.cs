using Company.Interface;
using MediatR;
using Comp = Company.Model.Company;
namespace Company.Queries.GetCompanyByIsin
{
    public class GetCompanyByIsinQueryHandler : IRequestHandler<GetCompanyByIsinQuery, Comp>
        
    {
        private readonly ICompanyRepository _companyRepository;
        public GetCompanyByIsinQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<Comp> Handle(GetCompanyByIsinQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetByIsin(request.InIs);
        }
    }
}
