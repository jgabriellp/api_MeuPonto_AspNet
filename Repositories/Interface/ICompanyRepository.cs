
using MeuPonto.Model;

namespace MeuPonto.Repositories.Interface
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(long id);
        Task<Company> GetByCnpjAsync(string cnpj);
        Task<Company> AddAsync(Company company);
        Task<bool> UpdateAsync(Company company);
        Task<bool> DeleteAsync(long id);
        Task<bool> CnpjExistsAsync(string cnpj);
    }
}
