using MeuPonto.Model.Dto.RequestDto;
using MeuPonto.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeuPonto.Services.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserRequestDto userRequestDto)
        {
            return null;
            //var jwtSettings = _configuration.GetSection("Jwt");
            //// Le a chave secreta e converte em bytes
            //var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

            //// 1. Definir as Claims (Dados a serem inseridos no Token)
            //var claims = new List<Claim>
            //{
            //    // Claims Padrão
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Email, user.Email),
            //    new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
            //    new Claim(ClaimTypes.Role, user.Role),
                
            //    // Claim Customizada (Útil para filtrar dados da empresa)
            //    new Claim("CompanyId", user.CompanyId.ToString())
            //};

            //// 2. Configurar o Token
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    // Emissor e Audiência lidos do appsettings.json
            //    Issuer = jwtSettings["Issuer"],
            //    Audience = jwtSettings["Audience"],

            //    // Expiração do token
            //    Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["DurationMinutes"]!)),

            //    // Assinatura com a chave secreta e o algoritmo HMACSHA256
            //    SigningCredentials = new SigningCredentials(
            //        new SymmetricSecurityKey(key),
            //        SecurityAlgorithms.HmacSha256Signature
            //    )
            //};

            //// 3. Gerar e Serializar o Token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return tokenHandler.WriteToken(token);
        }
    }
}
