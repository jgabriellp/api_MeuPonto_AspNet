using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Model
{
    public class AppUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
