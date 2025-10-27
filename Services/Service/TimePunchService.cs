using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Repositories.Interface;
using MeuPonto.Services.Interface;

namespace MeuPonto.Services.Service
{
    public class TimePunchService : ITimePunchService
    {
        private readonly ITimePunchRepository _timePunchRepository;
        private readonly IAppUserService _appUserService;

        public TimePunchService(ITimePunchRepository timePunchRepository, IAppUserService appUserService)
        {
            _timePunchRepository = timePunchRepository;
            _appUserService = appUserService;
        }

        public async Task<IEnumerable<TimePunch>> GetAllAsync()
        {
            return await _timePunchRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TimePunch>> GetAllByCompanyIdAsync(long companyId)
        {
            var timePunch = await _timePunchRepository.GetAllByCompanyIdAsync(companyId);
            if (timePunch == null)
            {
                return null;
            }
            return timePunch;
        }
        public async Task<IEnumerable<TimePunch>> GetAllByUserIdAsync(long userId)
        {
            var timePunches = await _timePunchRepository.GetAllByUserIdAsync(userId);
            if (timePunches == null)
            {
                return null;
            }

            return timePunches;
        }


        public Task<TimePunch?> GetByIdAsync(long id)
        {
            var timePunch = _timePunchRepository.GetByIdAsync(id);
            if (timePunch == null)
            {
                return null;
            }
            return timePunch;
        }

        public async Task<TimePunch> CreateAsync(TimePunchRequestDto timePunch)
        {
            var timePunchUser = await _appUserService.GetAppUserByIdAsync(timePunch.UserId);
            var timePunchCompany = await _timePunchRepository.GetAllByCompanyIdAsync(timePunch.CompanyId);
            
            if (timePunchUser == null || timePunchCompany == null)
            {
                return null;
            }

            var timePunchModel = new TimePunch
            {
                Timestamp = timePunch.Timestamp,
                Type = timePunch.Type,
                Location = timePunch.Location,
                PhotoUrl = timePunch.PhotoUrl,
                UserId = timePunch.UserId,
                CompanyId = timePunch.CompanyId
            };
            
            return await _timePunchRepository.CreateAsync(timePunchModel);
        }

        public async Task<bool> UpdateAsync(long id, TimePunchRequestDto timePunch)
        {
            var timePunchUser = await _appUserService.GetAppUserByIdAsync(timePunch.UserId);
            var timePunchCompany = await _timePunchRepository.GetAllByCompanyIdAsync(timePunch.CompanyId);
            var timePunchToUpdate = await _timePunchRepository.GetByIdAsync(id);

            if (timePunchUser == null || timePunchCompany == null || timePunchToUpdate == null)
            {
                return false;
            }

            var timePunchModel = new TimePunch
            {
                Id = id,
                Timestamp = timePunch.Timestamp,
                Type = timePunch.Type,
                Location = timePunch.Location,
                PhotoUrl = timePunch.PhotoUrl,
                UserId = timePunch.UserId,
                CompanyId = timePunch.CompanyId
            };

            return await _timePunchRepository.UpdateAsync(timePunchModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var timePunchToDelete = await _timePunchRepository.GetByIdAsync(id);
            if (timePunchToDelete == null)
            {
                return false;
            }

            return await _timePunchRepository.DeleteAsync(id);
        }
    }
}
