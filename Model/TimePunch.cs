namespace MeuPonto.Model
{
    public class TimePunch
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        // Tipo: 'IN' (ENTRADA) ou 'OUT' (SAIDA)
        public string Type { get; set; }

        public string Location { get; set; }
        public string PhotoUrl { get; set; }

        // Chaves estrangeiras (Foreign Keys)
        public long UserId { get; set; }
        public long CompanyId { get; set; }
    }
}
