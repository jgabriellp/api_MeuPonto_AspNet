namespace MeuPonto.Model.Dto.ResponseDto
{
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public long CompanyId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
