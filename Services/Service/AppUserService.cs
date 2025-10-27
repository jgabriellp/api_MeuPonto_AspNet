using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Model.Dto.ResponseDto;
using MeuPonto.Repositories.Interface;
using MeuPonto.Services.Interface;

namespace MeuPonto.Services.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly IUserRepository _appUserRepository;
        private readonly ICompanyRepository _companyRepository;

        public AppUserService(IUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        private UserResponseDto MapToResponse(AppUser user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Role = user.Role,
                Email = user.Email,
                Phone = user.Phone
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAppUsersAsync()
        {
            var users = await _appUserRepository.GetAllAsync();
            return users.Select(MapToResponse);
        }

        public async Task<UserResponseDto> GetAppUserByEmailAsync(string email)
        {
            var appUser = await _appUserRepository.GetByEmailAsync(email);
            if (appUser == null)
            {
                return null;
            }

            return MapToResponse(appUser);
        }

        public async Task<UserResponseDto> GetAppUserByIdAsync(long id)
        {
            var appUser = await _appUserRepository.GetByIdAsync(id);
            if (appUser == null)
            {
                return null;
            }

            return MapToResponse(appUser);
        }

        public async Task<UserResponseDto> CreateAppUserAsync(UserRequestDto user)
        {
            if (await _appUserRepository.GetByEmailAsync(user.Email) != null || await _companyRepository.GetByIdAsync(user.CompanyId) == null)
            {
                return null;
            }

            var newUser = new AppUser
            {
                Name = user.Name,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Role = user.Role,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone
            };

            return MapToResponse(await _appUserRepository.AddAsync(newUser));
        }

        public async Task<bool> UpdateAppUserAsync(long id, UserRequestDto user)
        {
            var appUser = await _appUserRepository.GetByIdAsync(id);
            if (appUser == null)
            {
                return false;
            }

            return await _appUserRepository.UpdateAsync(appUser);
        }

        public async Task<bool> DeleteAppUserAsync(long id)
        {
            var appUser = await _appUserRepository.GetByIdAsync(id);
            if (appUser == null)
            {
                return false;
            }

            return await _appUserRepository.DeleteAsync(id);
        }
    }
}
