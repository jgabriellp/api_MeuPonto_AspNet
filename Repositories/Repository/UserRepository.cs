using Dapper;
using MeuPonto.Data;
using MeuPonto.Model;
using MeuPonto.Repositories.Interface;

namespace MeuPonto.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly DapperContext _connection;

        public UserRepository(DapperContext connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            const string query = "SELECT * FROM AppUser";
            
            using (var connection = _connection.CreateConnection())
            {
                var users = await connection.QueryAsync<AppUser>(query);
                return users.ToList();
            }
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            const string query = "SELECT * FROM AppUser WHERE Email = @Email";

            using (var connection = _connection.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<AppUser>(query, new { Email = email });
                return user;
            }
        }

        public async Task<AppUser> GetByIdAsync(long id)
        {
            const string query = "SELECT * FROM AppUser WHERE Id = @Id";

            using (var connection = _connection.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<AppUser>(query, new { Id = id });
                return user;
            }
        }
        public async Task<AppUser> AddAsync(AppUser user)
        {
            const string query = @"
                INSERT INTO AppUser (Name, LastName, CompanyId, Role, Email, Password, Phone)
                VALUES (@Name, @LastName, @CompanyId, @Role, @Email, @Password, @Phone);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            using (var connection = _connection.CreateConnection())
            {                 
                var id = await connection.ExecuteScalarAsync<long>(query, user);
                user.Id = id;
                return user;
            }
        }

        public async Task<bool> UpdateAsync(AppUser user)
        {
            const string query = @"
                UPDATE AppUser
                SET Name = @Name,
                    CompanyId = @CompanyId,
                    Role = @Role,
                    Email = @Email,
                    Password = @Password,
                    Phone = @Phone
                WHERE Id = @Id";

            using (var connection = _connection.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, user);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            const string query = "DELETE FROM AppUser WHERE Id = @Id";

            using (var connection = _connection.CreateConnection())
            {                 
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }


    }
}
