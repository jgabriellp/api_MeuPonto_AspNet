using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;

namespace MeuPonto.Services.Interface
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> GetCompanyByIdAsync(long id);
        Task<Company> GetCompanyByCnpjAsync(string cnpj);
        Task<Company> CreateCompanyAsync(CompanyRequestDto company);
        Task<bool> UpdateCompanyAsync(long id, CompanyRequestDto company);
        Task<bool> DeleteCompanyAsync(long id);
    }
}
