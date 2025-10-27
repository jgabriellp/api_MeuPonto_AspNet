using Dapper;
using MeuPonto.Data;
using MeuPonto.Model;
using MeuPonto.Repositories.Interface;

namespace MeuPonto.Repositories.Repository
{
    public class TimePunchRepository : ITimePunchRepository
    {
        public readonly DapperContext _connection;

        public TimePunchRepository(DapperContext connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TimePunch>> GetAllAsync()
        {
            const string query = "SELECT * FROM TimePunch";
            using (var connection = _connection.CreateConnection())
            {
                var timePunches = await connection.QueryAsync<TimePunch>(query);
                return timePunches.ToList();
            }
        }

        public async Task<IEnumerable<TimePunch>> GetAllByCompanyIdAsync(long companyId)
        {
            const string query = "SELECT * FROM TimePunch WHERE CompanyId = @CompanyId";
            using (var connection = _connection.CreateConnection())
            {
                var timePunches = await connection.QueryAsync<TimePunch>(query, new { CompanyId = companyId });
                return timePunches.ToList();
            }
        }

        public async Task<IEnumerable<TimePunch>> GetAllByUserIdAsync(long userId)
        {
            const string query = "SELECT * FROM TimePunch WHERE UserId = @UserId";
            using (var connection = _connection.CreateConnection())
            {
                var timePunches = await connection.QueryAsync<TimePunch>(query, new { UserId = userId });
                return timePunches.ToList();
            }
        }

        public async Task<TimePunch?> GetByIdAsync(long id)
        {
            const string query = "SELECT * FROM TimePunch WHERE Id = @Id";
            using (var connection = _connection.CreateConnection())
            {
                var timePunch = await connection.QueryFirstOrDefaultAsync<TimePunch>(query, new { Id = id });
                return timePunch;
            }
        }

        public async Task<TimePunch> CreateAsync(TimePunch timePunch)
        {
            const string query = @"
                INSERT INTO TimePunch (Timestamp, Type, Location, PhotoUrl, UserId, CompanyId)
                VALUES (@Timestamp, @Type, @Location, @PhotoUrl, @UserId, @CompanyId);
                SELECT CAST(SCOPE_IDENTITY() as bigint);";

            using (var connection = _connection.CreateConnection())
            {
                var id = await connection.ExecuteScalarAsync<long>(query, timePunch);
                timePunch.Id = id;
                return timePunch;
            }
        }

        public async Task<TimePunch?> UpdateAsync(TimePunch timePunch)
        {
            const string query = @"
                UPDATE TimePunch
                SET Timestamp = @Timestamp,
                    Type = @Type,
                    Location = @Location,
                    PhotoUrl = @PhotoUrl,
                    UserId = @UserId,
                    CompanyId = @CompanyId
                WHERE Id = @Id";

            using (var connection = _connection.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, timePunch);
                return affectedRows > 0 ? timePunch : null;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            const string query = "DELETE FROM TimePunch WHERE Id = @Id";
            using (var connection = _connection.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }
    }
}
