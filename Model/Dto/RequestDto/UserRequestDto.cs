using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Model.Dto.RequestDto
{
    public class UserRequestDto
    {
        [Required] public string Name { get; set; }
        public string LastName { get; set; }
        [Required] public int CompanyId { get; set; }
        [Required] public string Role { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, MinLength(8)] public string Password { get; set; }
        public string Phone { get; set; }
    }
}
