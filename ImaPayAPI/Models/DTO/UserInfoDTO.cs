namespace ImaPayAPI.Models.DTO
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public int Agency { get; set; }
        public string Balance { get; set; } = string.Empty;
      
    }
}
