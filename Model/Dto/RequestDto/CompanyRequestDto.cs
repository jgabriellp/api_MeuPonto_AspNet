using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Model.Dto.RequestDto
{
    public class CompanyRequestDto
    {
        [Required] public string Nome { get; set; }
        [Required] public string Cnpj { get; set; }
        [EmailAddress] public string Email { get; set; }
    }
}
