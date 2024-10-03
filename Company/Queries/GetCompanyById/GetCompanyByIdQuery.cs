using MediatR;
using Comp = Company.Model.Company;
namespace Company.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<Comp>
    {
        public int Id { get; set; }
    }
}
