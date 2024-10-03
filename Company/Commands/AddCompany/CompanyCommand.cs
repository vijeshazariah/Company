using MediatR;
namespace Company.Commands.AddCompany

{
    public class CompanyCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public string Isin { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
