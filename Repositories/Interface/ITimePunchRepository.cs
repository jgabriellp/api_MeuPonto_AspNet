using MeuPonto.Model;

namespace MeuPonto.Repositories.Interface
{
    public interface ITimePunchRepository
    {
        Task<IEnumerable<TimePunch>> GetAllAsync();
        Task<IEnumerable<TimePunch>> GetAllByCompanyIdAsync(long companyId);
        Task<IEnumerable<TimePunch>> GetAllByUserIdAsync(long userId);
        Task<TimePunch?> GetByIdAsync(long id);
        Task<TimePunch> CreateAsync(TimePunch timePunch);
        Task<bool> UpdateAsync(TimePunch timePunch);
        Task<bool> DeleteAsync(long id);
    }
}
