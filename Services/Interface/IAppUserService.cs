using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Model.Dto.ResponseDto;

namespace MeuPonto.Services.Interface
{
    public interface IAppUserService
    {
        AuthResponseDto Login(LoginRequestDto loginRequestDto);
        Task<IEnumerable<UserResponseDto>> GetAllAppUsersAsync();
        Task<UserResponseDto> GetAppUserByIdAsync(long id);
        Task<UserResponseDto> GetAppUserByEmailAsync(string email);
        Task<UserResponseDto?> CreateAppUserAsync(UserRequestDto appUser);
        Task<bool> UpdateAppUserAsync(long id, UserRequestDto appUser);
        Task<bool> DeleteAppUserAsync(long id);
    }
}
