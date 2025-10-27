using MeuPonto.Model.Dto.RequestDto;

namespace MeuPonto.Services.Interface
{
    public interface ITokenService
    {
        string GenerateToken(UserRequestDto userRequestDto);
    }
}
