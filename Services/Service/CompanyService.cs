using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Repositories.Interface;
using MeuPonto.Services.Interface;

namespace MeuPonto.Services.Service
{
    public class CompanyService : ICompanyService
    {

        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<Company?> GetCompanyByCnpjAsync(string cnpj)
        {
            var company = await _companyRepository.GetByCnpjAsync(cnpj);
            if (company == null)
            {
                return null;
            }
            return company;
        }

        public async Task<Company?> GetCompanyByIdAsync(long id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return null;
            }
            return company;
        }

        public async Task<Company> CreateCompanyAsync(CompanyRequestDto company)
        {
            if (await _companyRepository.GetByCnpjAsync(company.Cnpj) != null)
            {
                return null;
            }

            var newCompany = new Company
            {
                Nome = company.Nome,
                Cnpj = company.Cnpj,
                Email = company.Email
            };

            return await _companyRepository.AddAsync(newCompany);
        }

        public async Task<bool> UpdateCompanyAsync(long id, CompanyRequestDto company)
        {
            if (await _companyRepository.GetByIdAsync(id) == null)
            {
                return false;
            }

            var newCompany = new Company
            {
                Id = id,
                Nome = company.Nome,
                Cnpj = company.Cnpj,
                Email = company.Email
            };

            return await _companyRepository.UpdateAsync(newCompany);
        }

        public async Task<bool> DeleteCompanyAsync(long id)
        {
            if (await _companyRepository.GetByIdAsync(id) == null)
            {
                return false;
            }

            return await _companyRepository.DeleteAsync(id);
        }
    }
}
