namespace ImaPayAPI.Models.DTO
{
    public class UserInfoDTO
    {
        public string Name { get; set; } = null!;
        public string Account { get; set; } = null!;
        public int Agency { get; set; }
        public int Balance { get; set; }
        public int Investments { get; set; }
        public int Savings { get; set; }
      
    }
}
