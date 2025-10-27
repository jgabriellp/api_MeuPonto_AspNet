using MeuPonto.Model;

namespace MeuPonto.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(long id);
        Task<AppUser> GetByEmailAsync(string email);
        Task<AppUser> AddAsync(AppUser user);
        Task<bool> UpdateAsync(AppUser user);
        Task<bool> DeleteAsync(long id);
    }
}
