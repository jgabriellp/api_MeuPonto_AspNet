using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;

namespace MeuPonto.Services.Interface
{
    public interface ITimePunchService
    {
        Task<IEnumerable<TimePunch>> GetAllAsync();
        Task<IEnumerable<TimePunch>> GetAllByCompanyIdAsync(long companyId);
        Task<IEnumerable<TimePunch>> GetAllByUserIdAsync(long userId);
        Task<TimePunch?> GetByIdAsync(long id);
        Task<TimePunch> CreateAsync(TimePunchRequestDto timePunch);
        Task<bool> UpdateAsync(long id, TimePunchRequestDto timePunch);
        Task<bool> DeleteAsync(long id);
    }
}
