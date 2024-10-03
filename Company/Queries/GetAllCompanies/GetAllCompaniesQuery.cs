using Company.DAL.DTO;
using MediatR;
using Comp = Company.Model.Company;
using CompanyDTO = Company.DAL.DTO.CompanyDTO;

namespace Company.Queries.GetAllCompanies
{
    public class GetAllCompaniesQuery:IRequest<List<CompanyDTO>>
    {
    }
}
