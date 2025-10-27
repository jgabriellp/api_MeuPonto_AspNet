using MeuPonto.Model;
using MeuPonto.Repositories.Interface;
using System.Data;
using Dapper;
using MeuPonto.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MeuPonto.Repositories.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _connection;

        public CompanyRepository(DapperContext connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            const string query = "SELECT Id, Nome, Cnpj, Email FROM Company";

            using (var connection = _connection.CreateConnection())
            {
                var company = await connection.QueryAsync<Company>(query);
                return company.ToList();
            }

        }

        public async Task<Company> GetByCnpjAsync(string cnpj)
        {
            const string query = "SELECT Id, Nome, Cnpj, Email FROM Company WHERE Cnpj = @Cnpj";

            using (var connection = _connection.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Company>(query, new { Cnpj = cnpj });
            }
        }

        public async Task<Company> GetByIdAsync(long id)
        {
            const string query = "SELECT Id, Nome, Cnpj, Email FROM Company WHERE Id = @Id";
            using (var connection = _connection.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Company>(query, new { Id = id });
            }
        }

        public async Task<Company> AddAsync(Company company)
        {
            const string query = @"
                INSERT INTO Company (Nome, Cnpj, Email)
                VALUES (@Nome, @Cnpj, @Email);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            using (var connection = _connection.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<long>(query, company);
                company.Id = id;
            
                return company;
            }
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            const string query = @"
                UPDATE Company
                SET Nome = @Nome,
                    Cnpj = @Cnpj,
                    Email = @Email
                WHERE Id = @Id";
            using (var connection = _connection.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, company);
                
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            const string query = "DELETE FROM Company WHERE Id = @Id";
            using (var connection = _connection.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }

        public async Task<bool> CnpjExistsAsync(string cnpj)
        {
            using (var connection = _connection.CreateConnection())
            {
                const string query = "SELECT COUNT(1) FROM Company WHERE Cnpj = @Cnpj";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Cnpj = cnpj });
                return count > 0;
            }
        }

    }
}
