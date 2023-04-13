namespace ImaPayAPI.Models.DTO
{
    public class TranscationDTO
    {
        public DateTime Date { get; set; }
        public string Agency { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal ValueTransaction { get; set; }
    }
}
