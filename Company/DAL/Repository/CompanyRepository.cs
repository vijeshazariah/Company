using Company.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using CompanyModel = Company.Model.Company;
namespace Company.DAL.Repository
{

    public class CompanyRepository: ICompanyRepository
    {
        private readonly string _connectionString;

        public CompanyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DapperCompanyDb");
        }
        public async Task<int> CreateCompanyAsync(CompanyModel company)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Name", company.Name);
                parameters.Add("StockTicker", company.StockTicker);
                parameters.Add("Exchange", company.Exchange);
                parameters.Add("Isin", company.Isin);
                parameters.Add("WebsiteUrl", company.WebsiteUrl);

                return await connection.ExecuteAsync("sp_InsertCompany", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task UpdateCompanyAsync(CompanyModel company)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", company.Id);
                parameters.Add("Name", company.Name);
                parameters.Add("StockTicker", company.StockTicker);
                parameters.Add("Exchange", company.Exchange);
                parameters.Add("Isin", company.Isin);
                parameters.Add("WebsiteUrl", company.WebsiteUrl);
                await connection.ExecuteAsync("sp_UpdateCompany", parameters, commandType: CommandType.StoredProcedure);
            }

        }
        public async Task<CompanyModel> GetCompanyByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection?.QueryFirstOrDefaultAsync<CompanyModel>("sp_GetCompanyById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<CompanyModel> GetByIsin(string isIN)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<CompanyModel>("sp_GetCompanyByIsin", new { Isin = isIN }, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<List<CompanyModel>> GetAllCompanies()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var  allCompanies = (await connection.QueryAsync<CompanyModel>(
                    "sp_GetAllCompanies",
                    commandType: CommandType.StoredProcedure)).ToList();

                return allCompanies;
            }
        }
        
    }
}
