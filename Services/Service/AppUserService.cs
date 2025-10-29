
using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Model.Dto.ResponseDto;
using MeuPonto.Repositories.Interface;
using MeuPonto.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeuPonto.Services.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly IUserRepository _appUserRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _configuration;

        public AppUserService(IUserRepository appUserRepository, ICompanyRepository companyRepository, IConfiguration configuration)
        {
            _appUserRepository = appUserRepository;
            _companyRepository = companyRepository;
            _configuration = configuration;
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

        public AuthResponseDto Login(LoginRequestDto loginRequestDto)
        {
            var user = _appUserRepository.GetByEmailAsync(loginRequestDto.Email);
            if (user == null || user.Result == null || !BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.Result.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Result.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string userToken = tokenHandler.WriteToken(token);

            var userAccess = new AuthResponseDto
            {
                Id = user.Result.Id,
                Name = user.Result.Name,
                Email = user.Result.Email,
                Role = user.Result.Role,
                Token = userToken
            };

            return userAccess;
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

        public async Task<UserResponseDto?> CreateAppUserAsync(UserRequestDto user)
        {
            if (await _appUserRepository.GetByEmailAsync(user.Email) != null || await _companyRepository.GetByIdAsync(user.CompanyId) == null)
            {
                return null;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new AppUser
            {
                Name = user.Name,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Role = user.Role,
                Email = user.Email,
                Password = hashedPassword,
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
