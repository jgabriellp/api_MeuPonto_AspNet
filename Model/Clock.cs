namespace MeuPonto.Model
{
    public class Clock
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CompanyId { get; set; }
        public string PicturePath { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; } // e.g., "IN" or "OUT"
    }
}
