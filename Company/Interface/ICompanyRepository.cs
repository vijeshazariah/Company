using CompanyModel = Company.Model.Company;
namespace Company.Interface
{
    public interface ICompanyRepository
    {
        Task<int> CreateCompanyAsync(Company.Model.Company company);
        Task<CompanyModel> GetCompanyByIdAsync(int id);
        Task<CompanyModel> GetByIsin(string isIN);
        Task<List<CompanyModel>> GetAllCompanies();
        Task UpdateCompanyAsync(CompanyModel existingCompany);
        bool ValidateIsin(string isiN);
    }
}
