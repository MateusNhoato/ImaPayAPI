namespace ImaPayAPI.Models.DTO
{
    public class UserInfoDTO
    {
        public string Name { get; set; } = null!;
        public string Account { get; set; } = null!;
        public string Agency { get; set; } = null!;
        public decimal Balance { get; set; }
        public decimal Investments { get; set; }
        public decimal Savings { get; set; }
      
    }
}
