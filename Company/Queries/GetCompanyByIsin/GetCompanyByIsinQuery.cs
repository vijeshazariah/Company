using MediatR;
using Comp = Company.Model.Company;

namespace Company.Queries.GetCompanyByIsin
{
    public class GetCompanyByIsinQuery: IRequest<Comp>
    {
        public string InIs { get; set; }
    }
}
