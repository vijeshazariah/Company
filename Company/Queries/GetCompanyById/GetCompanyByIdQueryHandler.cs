using Company.Interface;
using MediatR;
using Comp = Company.Model.Company;

namespace Company.Queries.GetCompanyById
{
    public class GetCompanyByIdQueryHandler: IRequestHandler<GetCompanyByIdQuery, Comp>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Comp> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetCompanyByIdAsync(request.Id);
        }
    }
}
