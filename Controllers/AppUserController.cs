using MeuPonto.Model;
using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Model.Dto.ResponseDto;
using MeuPonto.Repositories.Interface;
using MeuPonto.Services.Interface;
using MeuPonto.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPonto.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var userAccess = _appUserService.Login(loginRequestDto);
            if (userAccess == null || userAccess.Token == string.Empty)
            {
                return BadRequest(new { message = "UserEmail or Password is incorrect" });
            }
            return Ok(userAccess);
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponseDto>> GetAllAppUsersAsync()
        {
            return await _appUserService.GetAllAppUsersAsync();
        }

        [HttpGet("{id}", Name = "GetAppUserById")]
        public async Task<IActionResult> GetAppUserByIdAsync(long id)
        {
            var UserResponseDto = await _appUserService.GetAppUserByIdAsync(id);
            if (UserResponseDto == null)
            {
                return NotFound();
            }
            return Ok(UserResponseDto);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetAppUserByEmailAsync(string email)
        {
            var UserResponseDto = await _appUserService.GetAppUserByEmailAsync(email);
            if (UserResponseDto == null)
            {
                return NotFound();
            }
            return Ok(UserResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUserAsync([FromBody] UserRequestDto appUser)
        {
            var UserResponseDto = await _appUserService.CreateAppUserAsync(appUser);
            if(UserResponseDto == null)
            {
                return BadRequest("User already created or the companyId doesn't exist.");
            }
            return CreatedAtAction("GetAppUserById", new { id = UserResponseDto.Id }, UserResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppUserAsync(long id, [FromBody] UserRequestDto appUser)
        {
            var updated = await _appUserService.UpdateAppUserAsync(id, appUser);
            if (!updated)
            {
                return BadRequest("User could not be updated.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUserAsync(long id)
        {
            var deleted = await _appUserService.DeleteAppUserAsync(id);
            if (!deleted)
            {
                return BadRequest("User could not be deleted.");
            }
            return NoContent();
        }
    }
}
