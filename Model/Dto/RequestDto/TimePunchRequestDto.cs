using System.ComponentModel.DataAnnotations;

namespace MeuPonto.Model.Dto.RequestDto
{
    public class TimePunchRequestDto
    {
        public DateTime Timestamp { get; set; }

        [Required] public string Type { get; set; }
        [Required] public string Location { get; set; }
        [Required] public string PhotoUrl { get; set; }

        // Chaves estrangeiras (Foreign Keys)
        [Required] public long UserId { get; set; }
        [Required] public long CompanyId { get; set; }
    }
}
